using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core;
using ModelContextProtocol.NET.Core.Models.JsonRpc;
using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Responses;
using ModelContextProtocol.NET.Core.Transport.Base;
using ModelContextProtocol.NET.Server.Contexts;
using ModelContextProtocol.NET.Server.Features.Prompts;
using ModelContextProtocol.NET.Server.Features.Resources;
using ModelContextProtocol.NET.Server.Features.Tools;

namespace ModelContextProtocol.NET.Server.Session;

/// <summary>
/// Represents a server-side MCP session.
/// Handles initialization, message routing, and session state.
/// </summary>
internal class McpServerSession(
    IServiceProvider serviceProvider,
    ILogger<McpServerSession> logger,
    IMcpTransportBase transport,
    Implementation serverInfo,
    ResourceSubscriptionManager subscriptionManager
) : IAsyncDisposable
{
    private readonly CancellationTokenSource sessionCts = new();
    private readonly TaskCompletionSource initializationCompletion = new();
    private readonly Dictionary<string, IToolHandler> toolHandlers = [];
    private readonly Dictionary<string, IResourceHandler> resourceHandlers = [];
    private readonly Dictionary<string, IPromptHandler> promptHandlers = [];

    private SessionState state = SessionState.Created;
    private Implementation? clientInfo;
    private readonly HashSet<string> activeSubscriptions = [];

    private IServiceScope? sessionScope;

    public static ServerCapabilities DefaultServerCapabilities =>
        new()
        {
            Tools = new ToolsCapability { ListChanged = true },
            Resources = new ResourcesCapability { ListChanged = true, Subscribe = true },
            Prompts = new PromptsCapability { ListChanged = true },
        };

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (state != SessionState.Created)
        {
            throw new InvalidOperationException($"Cannot start session in state {state}");
        }

        try
        {
            UpdateState(SessionState.Starting);

            // Link the cancellation tokens
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(
                sessionCts.Token,
                cancellationToken
            );

            UpdateState(SessionState.WaitingForInitialization);

            // Start message processing
            _ = ProcessMessagesAsync(cts.Token);

            // Wait for initialization to complete
            await initializationCompletion.Task.WaitAsync(cts.Token);

            UpdateState(SessionState.Running);
        }
        catch (Exception ex)
        {
            UpdateState(SessionState.Failed);
            logger.LogError(ex, "Failed to start session");
            throw;
        }
    }

    public void Stop()
    {
        try
        {
            // Clean up subscriptions
            foreach (var uri in activeSubscriptions)
            {
                subscriptionManager.Unsubscribe(uri, clientInfo?.Name ?? string.Empty);
            }
            activeSubscriptions.Clear();

            // Cancel ongoing operations
            _ = DisposeAsync();
            UpdateState(SessionState.Stopped);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during session cleanup");
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        Stop();
        sessionCts.Dispose();
        await transport.DisposeAsync();
        sessionScope?.Dispose();
    }

    private async Task ProcessMessagesAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing messages...");
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await transport.ReadMessageAsync(cancellationToken);
                if (message == null)
                    continue;

                // Handle initialization first
                if (state == SessionState.WaitingForInitialization)
                {
                    if (message is InitializeRequest initRequest)
                    {
                        await HandleInitializeRequestAsync(initRequest, cancellationToken);
                        continue;
                    }

                    // Reject any non-initialize requests during initialization
                    if (message is IJsonRpcRequest request)
                    {
                        await SendErrorResponseAsync(
                            new JsonRpcError
                            {
                                Id = request.Id,
                                Error = new JsonRpcErrorData
                                {
                                    Code = McpErrorCodes.NotInitialized,
                                    Message = "Not initialized"
                                }
                            },
                            cancellationToken
                        );
                    }
                    continue;
                }

                // Handle cancellation notifications
                if (message is CancelledNotification cancelNotification)
                {
                    HandleCancellation(cancelNotification);
                    continue;
                }

                // Route the message based on type
                await RouteMessageAsync(message, cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Normal cancellation, ignore
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing messages");
            UpdateState(SessionState.Failed);
            throw;
        }
    }

    private void HandleCancellation(CancelledNotification notification)
    {
        if (notification.Params?.RequestId != null)
        {
            // TODO: Implement request cancellation
            // This would involve keeping track of ongoing requests and their cancellation tokens
            logger.LogWarning(
                "Received cancellation for request {RequestId}, but cancellation is not yet implemented",
                notification.Params.RequestId
            );
        }
    }

    private async Task HandleInitializeRequestAsync(
        InitializeRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (request.Params == null)
            {
                var error = new JsonRpcError
                {
                    Id = request.Id,
                    Error = new JsonRpcErrorData
                    {
                        Code = McpErrorCodes.InvalidParams,
                        Message = "Invalid parameters"
                    }
                };
                await transport.WriteMessageAsync(error, cancellationToken);
                return;
            }

            // Store client info
            clientInfo = request.Params.ClientInfo;

            // Create session scope
            sessionScope = serviceProvider.CreateScope();
            var scopedProvider = sessionScope.ServiceProvider;

            // Register session-scoped services
            var services = new ServiceCollection();
            services.AddSingleton(serverInfo);
            services.AddSingleton<ServerContext>();
            services.AddSingleton<IServerContext, ServerContext>();
            services.AddSingleton((SessionContext)request.Params);
            services.AddSingleton<ISessionContext, SessionContext>();
            services.AddSingleton<FeatureContext>();
            services.AddSingleton<IFeatureContext, FeatureContext>();

            // Initialize handlers from scoped provider
            foreach (var toolHandler in scopedProvider.GetServices<IToolHandler>())
            {
                toolHandlers[toolHandler.Tool.Name] = toolHandler;
            }
            foreach (var resourceHandler in scopedProvider.GetServices<IResourceHandler>())
            {
                resourceHandlers[resourceHandler.Template.UriTemplate] = resourceHandler;
            }
            foreach (var promptHandler in scopedProvider.GetServices<IPromptHandler>())
            {
                promptHandlers[promptHandler.Template.Name] = promptHandler;
            }

            // Send initialize result
            var response = new JsonRpcResponse<InitializeResult>
            {
                Id = request.Id,
                Result = new InitializeResult
                {
                    ProtocolVersion = Constants.LATEST_PROTOCOL_VERSION,
                    ServerInfo = serverInfo,
                    Capabilities = DefaultServerCapabilities
                }
            };

            await transport.WriteMessageAsync(response, cancellationToken);

            // Complete initialization
            initializationCompletion.TrySetResult();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to handle initialize request");
            await SendErrorResponseAsync(
                new JsonRpcError
                {
                    Id = request.Id,
                    Error = new JsonRpcErrorData
                    {
                        Code = McpErrorCodes.InternalError,
                        Message = ex.Message
                    }
                },
                cancellationToken
            );
            initializationCompletion.TrySetException(ex);
        }
    }

    private async Task RouteMessageAsync(
        JsonRpcMessage message,
        CancellationToken cancellationToken
    )
    {
        try
        {
            switch (message)
            {
                case ListPromptsRequest request:
                    await HandleListPromptsRequestAsync(request, cancellationToken);
                    break;

                case GetPromptRequest request:
                    await HandleGetPromptRequestAsync(request, cancellationToken);
                    break;

                case CallToolRequest request:
                    try
                    {
                        await HandleCallToolRequestAsync(request, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        await SendErrorResponseAsync(
                            new JsonRpcError
                            {
                                Error = new JsonRpcErrorData
                                {
                                    Code = McpErrorCodes.InvalidParams,
                                    Message = ex.Message
                                },
                                Id = request.Id
                            },
                            cancellationToken
                        );
                    }
                    break;

                case ListResourcesRequest request:
                    await HandleListResourcesRequestAsync(request, cancellationToken);
                    break;

                case ListToolsRequest request:
                    await HandleListToolsRequestAsync(request, cancellationToken);
                    break;

                case ReadResourceRequest request:
                    await HandleReadResourceRequestAsync(request, cancellationToken);
                    break;

                case SubscribeRequest request:
                    await HandleSubscribeRequestAsync(request, cancellationToken);
                    break;

                case UnsubscribeRequest request:
                    await HandleUnsubscribeRequestAsync(request, cancellationToken);
                    break;

                case IJsonRpcRequest request:
                    await SendErrorResponseAsync(
                        new JsonRpcError
                        {
                            Id = request.Id,
                            Error = new JsonRpcErrorData
                            {
                                Code = McpErrorCodes.MethodNotFound,
                                Message = "Method not found"
                            }
                        },
                        cancellationToken
                    );
                    break;

                case IJsonRpcNotification:
                    // Handle notifications if needed
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error routing message");
            if (message is IJsonRpcRequest request)
            {
                await SendErrorResponseAsync(
                    new JsonRpcError
                    {
                        Id = request.Id,
                        Error = new JsonRpcErrorData
                        {
                            Code = McpErrorCodes.InternalError,
                            Message = ex.Message
                        }
                    },
                    cancellationToken
                );
            }
        }
    }

    private async Task HandleListPromptsRequestAsync(
        ListPromptsRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = new ListPromptsResult
        {
            Prompts = [.. promptHandlers.Values.Select(h => h.Template)]
        };

        await SendResponseAsync(request.Id, result, cancellationToken);
    }

    private async Task HandleGetPromptRequestAsync(
        GetPromptRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request.Params?.Arguments, nameof(request.Params));
        if (!promptHandlers.TryGetValue(request.Params.Name, out var handler))
        {
            throw new ArgumentException($"Prompt '{request.Params.Name}' not found");
        }

        var messages = await handler.HandleAsync(request.Params.Arguments, cancellationToken);
        var result = new GetPromptResult { Messages = messages };

        await SendResponseAsync(request.Id, result, cancellationToken);
    }

    private async Task HandleCallToolRequestAsync(
        CallToolRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request.Params?.Arguments, nameof(request.Params));
        if (!toolHandlers.TryGetValue(request.Params.Name, out var handler))
        {
            throw new ArgumentException($"Tool '{request.Params.Name}' not found");
        }

        var result = await handler.HandleAsync(request.Params.Arguments, cancellationToken);
        await SendResponseAsync(request.Id, result, cancellationToken);
    }

    private async Task HandleListToolsRequestAsync(
        ListToolsRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = new ListToolsResult { Tools = [.. toolHandlers.Values.Select(h => h.Tool)] };
        await SendResponseAsync(request.Id, result, cancellationToken);
    }

    private async Task HandleListResourcesRequestAsync(
        ListResourcesRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = new ListResourcesResult
        {
            Resources =
            [
                .. resourceHandlers
                    .Values.Select(h => h.Template)
                    .Select(rt => new Resource
                    {
                        Name = rt.Name,
                        Uri = rt.UriTemplate,
                        Annotations = rt.Annotations,
                        Description = rt.Description,
                        MimeType = rt.MimeType
                    })
            ]
        };
        await SendResponseAsync(request.Id, result, cancellationToken);
    }

    private async Task HandleReadResourceRequestAsync(
        ReadResourceRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request.Params, nameof(request.Params));

        var handler =
            resourceHandlers.Values.FirstOrDefault(h =>
                UriTemplateMatches(h.Template.UriTemplate, request.Params.Uri)
            )
            ?? throw new ArgumentException($"No handler found for resource '{request.Params.Uri}'");

        var parameters = ExtractUriParameters(handler.Template.UriTemplate, request.Params.Uri);
        var contents = await handler.HandleAsync(parameters, cancellationToken);

        // If this resource has subscribers, notify them of the update
        if (subscriptionManager.HasSubscribers(request.Params.Uri))
        {
            await subscriptionManager.NotifyUpdateAsync(
                request.Params.Uri,
                contents,
                cancellationToken
            );
        }

        var result = new ReadResourceResult { Contents = contents };
        await SendResponseAsync(request.Id, result, cancellationToken);
    }

    private async Task HandleSubscribeRequestAsync(
        SubscribeRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request.Params, nameof(request.Params));

        // Find the handler for this URI
        var handler =
            resourceHandlers.Values.FirstOrDefault(h =>
                UriTemplateMatches(h.Template.UriTemplate, request.Params.Uri)
            )
            ?? throw new ArgumentException($"No handler found for resource '{request.Params.Uri}'");

        // Subscribe to the resource
        subscriptionManager.Subscribe(request.Params.Uri, clientInfo!.Name);
        activeSubscriptions.Add(request.Params.Uri);

        // Get initial value
        var parameters = ExtractUriParameters(handler.Template.UriTemplate, request.Params.Uri);
        var contents = await handler.HandleAsync(parameters, cancellationToken);

        // Send initial value as notification
        await subscriptionManager.NotifyUpdateAsync(
            request.Params.Uri,
            contents,
            cancellationToken
        );

        // Send success response
        await SendResponseAsync(request.Id, new EmptyResult(), cancellationToken);
    }

    private async Task HandleUnsubscribeRequestAsync(
        UnsubscribeRequest request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request.Params, nameof(request.Params));

        // Unsubscribe from the resource
        subscriptionManager.Unsubscribe(request.Params.Uri, clientInfo!.Name);
        activeSubscriptions.Remove(request.Params.Uri);

        // Send success response
        await SendResponseAsync(request.Id, new EmptyResult(), cancellationToken);
    }

    private async Task SendResponseAsync<T>(
        RpcId RpcId,
        T result,
        CancellationToken cancellationToken
    )
    {
        var response = new JsonRpcResponse<T> { Id = RpcId, Result = result };
        await transport.WriteMessageAsync(response, cancellationToken);
    }

    private async Task SendErrorResponseAsync(
        JsonRpcError error,
        CancellationToken cancellationToken
    ) => await transport.WriteMessageAsync(error, cancellationToken);

    private static bool UriTemplateMatches(string template, string uri) =>
        UriTemplate.IsMatch(template, uri);

    private static Dictionary<string, string> ExtractUriParameters(string template, string uri) =>
        UriTemplate.ExtractParameters(template, uri);

    private void UpdateState(SessionState newState)
    {
        var oldState = state;
        state = newState;
        logger.LogInformation(
            "Session state changed from {OldState} to {NewState}",
            oldState,
            newState
        );
    }
}

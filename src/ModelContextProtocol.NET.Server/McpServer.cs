using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Transport.Base;
using ModelContextProtocol.NET.Server.Features.Resources;
using ModelContextProtocol.NET.Server.Session;

namespace ModelContextProtocol.NET.Server;

/// <summary>
/// Default implementation of an MCP server.
/// </summary>
internal class McpServer(
    IServiceProvider serviceProvider,
    ILogger<McpServer> logger,
    IEnumerable<IMcpTransportBase> transports
) : IMcpServer
{
    private readonly ConcurrentDictionary<Guid, McpServerSession> sessions = new();
    private readonly ConcurrentDictionary<IMcpTransportBase, Task> acceptConnectionTasks = [];
    private readonly ConcurrentDictionary<Guid, Task> handleSessionTasks = [];
    private readonly CancellationTokenSource serverCts = new();
    private bool isDisposed;

    public Implementation ServerInfo { get; } =
        serviceProvider.GetRequiredService<Implementation>();

    public void Start(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Starting MCP server...");

        // Link cancellation tokens
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(
            serverCts.Token,
            cancellationToken
        );

        // Start all transports
        foreach (var transport in transports)
            acceptConnectionTasks[transport] = AcceptConnectionsAsync(transport, cts.Token);

        logger.LogInformation("MCP server started");
    }

    public void Stop(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Stopping MCP server...");

        // Cancel all ongoing operations
        serverCts.Cancel();

        // Stop all sessions
        foreach (var session in sessions.Values)
            session.Stop();

        logger.LogInformation("MCP server stopped");
    }

    public async ValueTask DisposeAsync()
    {
        if (!isDisposed)
        {
            Stop();

            serverCts.Dispose();

            if (serviceProvider is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
            else if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

            isDisposed = true;
        }
    }

    private async Task AcceptConnectionsAsync(
        IMcpTransportBase transport,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await transport.ConnectAsync(cancellationToken);

            // Create a new session
            var sessionId = Guid.NewGuid();
            var session = new McpServerSession(
                serviceProvider,
                serviceProvider.GetRequiredService<ILogger<McpServerSession>>(),
                transport,
                ServerInfo,
                serviceProvider.GetRequiredService<ResourceSubscriptionManager>()
            );

            // Store and start the session
            if (sessions.TryAdd(sessionId, session))
                handleSessionTasks[sessionId] = HandleSessionAsync(
                    sessionId,
                    session,
                    cancellationToken
                );
            else
                // This should never happen, but just in case
                await transport.DisposeAsync();
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Normal cancellation, ignore
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error accepting connections on transport {Transport}", transport);
        }
    }

    private async Task HandleSessionAsync(
        Guid sessionId,
        McpServerSession session,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await session.StartAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in session {SessionId}", sessionId);
        }
    }
}

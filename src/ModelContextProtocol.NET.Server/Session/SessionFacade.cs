using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;
using ModelContextProtocol.NET.Server.Contexts;
using ModelContextProtocol.NET.Server.Features.Prompts;
using ModelContextProtocol.NET.Server.Features.Resources;
using ModelContextProtocol.NET.Server.Features.Tools;

namespace ModelContextProtocol.NET.Server.Session;

/// <summary>
/// Facade for managing session-related operations.
/// </summary>
public class SessionFacade(IServiceProvider serviceProvider)
    : IDisposable,
        ISessionFacade,
        IInternalSessionFacade
{
    private IServiceScope? sessionScope;
    private ISessionContext? sessionContext;
    private Dictionary<string, IToolHandler>? toolHandlers;
    private Dictionary<string, IResourceHandler>? resourceHandlers;
    private Dictionary<string, IPromptHandler>? promptHandlers;
    private readonly HashSet<string> activeSubscriptions = [];

    void IInternalSessionFacade.Initialize(InitializeRequest.Parameters parameters)
    {
        // Create session scope
        sessionScope = serviceProvider.CreateScope();
        var scopedProvider = sessionScope.ServiceProvider;

        // Initialize session context
        sessionContext = (SessionContext)parameters;

        // Initialize handlers
        toolHandlers = [];
        resourceHandlers = [];
        promptHandlers = [];

        foreach (var handler in scopedProvider.GetServices<IToolHandler>())
        {
            toolHandlers[handler.Tool.Name] = handler;
        }
        foreach (var handler in scopedProvider.GetServices<IResourceHandler>())
        {
            resourceHandlers[handler.Template.UriTemplate] = handler;
        }
        foreach (var handler in scopedProvider.GetServices<IPromptHandler>())
        {
            promptHandlers[handler.Template.Name] = handler;
        }
    }

    IReadOnlyDictionary<string, IToolHandler> IInternalSessionFacade.ToolHandlers =>
        toolHandlers as IReadOnlyDictionary<string, IToolHandler>
        ?? throw new InvalidOperationException("Session not initialized");

    IReadOnlyDictionary<string, IResourceHandler> IInternalSessionFacade.ResourceHandlers =>
        resourceHandlers as IReadOnlyDictionary<string, IResourceHandler>
        ?? throw new InvalidOperationException("Session not initialized");

    IReadOnlyDictionary<string, IPromptHandler> IInternalSessionFacade.PromptHandlers =>
        promptHandlers as IReadOnlyDictionary<string, IPromptHandler>
        ?? throw new InvalidOperationException("Session not initialized");

    public ISessionContext Context =>
        sessionContext ?? throw new InvalidOperationException("Session not initialized");

    IServiceScope IInternalSessionFacade.SessionScope =>
        sessionScope ?? throw new InvalidOperationException("Session not initialized");

    ISet<string> IInternalSessionFacade.ActiveSubscriptions => activeSubscriptions;

    public void Dispose() { }
}

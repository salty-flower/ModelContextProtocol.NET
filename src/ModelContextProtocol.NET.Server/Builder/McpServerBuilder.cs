using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Transport.Base;
using ModelContextProtocol.NET.Server.Features.Resources;
using ModelContextProtocol.NET.Server.Transport;

namespace ModelContextProtocol.NET.Server.Builder;

/// <summary>
/// Builder for configuring and creating an MCP server.
/// </summary>
public sealed class McpServerBuilder(Implementation serverInfo)
{
    private readonly ServiceCollection internalServiceCollection = [];
    private readonly ServiceCollection userServiceCollection = [];
    private readonly List<Action<IServiceCollection>> userServiceConfigureActions = [];
    private bool userHasConfiguredLogger = false;

    /// <summary>
    /// Configures logging for the server. If not configured, no logging will occur.
    /// </summary>
    public McpServerBuilder ConfigureLogging(Action<ILoggingBuilder> configure)
    {
        internalServiceCollection.AddLogging(configure);
        userHasConfiguredLogger = true;
        return this;
    }

    /// <summary>
    /// Configures services for the server.
    /// </summary>
    public McpServerBuilder ConfigureUserServices(Action<IServiceCollection> configure)
    {
        userServiceConfigureActions.Add(configure);
        return this;
    }

    /// <summary>
    /// Adds user-provided services to the server.
    /// </summary>
    public McpServerBuilder AddUserServices(IServiceCollection userServices)
    {
        userServiceCollection.Add(userServices);
        return this;
    }

    public McpServerBuilder AddStdioTransport()
    {
        internalServiceCollection.AddSingleton<IMcpTransportBase, StdioServerTransport>();
        return this;
    }

    public McpServerBuilder AddWebSocketTransport()
    {
        internalServiceCollection.AddSingleton<IMcpTransportBase, WebSocketServerTransport>();
        return this;
    }

    /// <summary>
    /// Configures tools for the server.
    /// </summary>
    public McpServerBuilder ConfigureTools(Action<IToolRegistry> configure) =>
        ConfigureUserServices(services => configure(new ToolRegistry(services)));

    /// <summary>
    /// Configures resources for the server.
    /// </summary>
    public McpServerBuilder ConfigureResources(Action<IResourceRegistry> configure) =>
        ConfigureUserServices(services => configure(new ResourceRegistry(services)));

    /// <summary>
    /// Configures prompts for the server.
    /// </summary>
    public McpServerBuilder ConfigurePrompts(Action<IPromptRegistry> configure) =>
        ConfigureUserServices(services => configure(new PromptRegistry(services)));

    /// <summary>
    /// Builds the server.
    /// </summary>
    public IMcpServer Build()
    {
        if (!userHasConfiguredLogger)
        {
            internalServiceCollection.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            internalServiceCollection.AddScoped(typeof(ILogger<>), typeof(NullLogger<>));
        }

        // Apply all configuration actions
        foreach (var configure in userServiceConfigureActions)
            configure(userServiceCollection);

        internalServiceCollection.AddTransient<ResourceSubscriptionManager>();

        var internalServiceProvider = internalServiceCollection.BuildServiceProvider();

        // Create server
        return new McpServer(
            internalServiceProvider,
            userServiceCollection,
            serverInfo,
            internalServiceProvider.GetRequiredService<ILogger<McpServer>>(),
            internalServiceProvider.GetServices<IMcpTransportBase>()
        );
    }
}

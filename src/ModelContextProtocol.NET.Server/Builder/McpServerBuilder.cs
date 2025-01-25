using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Transport.Base;
using ModelContextProtocol.NET.Server.Contexts;
using ModelContextProtocol.NET.Server.Features.Resources;
using ModelContextProtocol.NET.Server.Session;
using ModelContextProtocol.NET.Server.Transport;

namespace ModelContextProtocol.NET.Server.Builder;

/// <summary>
/// Builder for configuring and creating an MCP server.
/// </summary>
public sealed class McpServerBuilder
{
    public IServiceCollection Services { get; private init; }
    public IToolRegistry Tools { get; private init; }
    public IResourceRegistry Resources { get; private init; }
    public IPromptRegistry Prompts { get; private init; }

    public McpServerBuilder(Implementation serverInfo)
        : this(serverInfo, new ServiceCollection()) { }

    public McpServerBuilder(Implementation serverInfo, IServiceCollection services)
    {
        Services = services
            // Register server-wide services
            .AddSingleton(serverInfo)
            .AddSingleton<IMcpServer, McpServer>()
            .AddSingleton<McpServerSession>()
            .AddSingleton<SessionFacade>()
            .AddSingleton<ISessionFacade, SessionFacade>()
            .AddSingleton<IInternalSessionFacade, SessionFacade>()
            .AddSingleton<ResourceSubscriptionManager>()
            // Register session-scoped services
            .AddScoped<ServerContext>()
            .AddScoped<IServerContext, ServerContext>()
            .AddScoped<FeatureContext>()
            .AddScoped<IFeatureContext, FeatureContext>();

        // Initialize registries
        Tools = new ToolRegistry(Services);
        Resources = new ResourceRegistry(Services);
        Prompts = new PromptRegistry(Services);
    }

    public McpServerBuilder AddStdioTransport()
    {
        Services.AddSingleton<IMcpTransportBase, StdioServerTransport>();
        return this;
    }

    public McpServerBuilder AddWebSocketTransport()
    {
        Services.AddSingleton<IMcpTransportBase, WebSocketServerTransport>();
        return this;
    }

    /// <summary>
    /// Builds the server.
    /// </summary>
    public IMcpServer Build()
    {
        if (!Services.Any(d => d.ServiceType == typeof(ILoggerFactory)))
        {
            Services
                .AddSingleton<ILoggerFactory, NullLoggerFactory>()
                .AddScoped(typeof(ILogger<>), typeof(NullLogger<>));
        }

        // Create server
        return Services.BuildServiceProvider().GetRequiredService<IMcpServer>();
    }
}

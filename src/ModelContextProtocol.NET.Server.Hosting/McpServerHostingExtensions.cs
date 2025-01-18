using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Server.Builder;

namespace ModelContextProtocol.NET.Server.Hosting;

/// <summary>
/// Extension methods for configuring MCP server hosting.
/// </summary>
public static class McpServerHostingExtensions
{
    /// <summary>
    /// Adds MCP server to the service collection.
    /// </summary>
    /// <remarks>
    /// By default, <seealso cref="Microsoft.Extensions.Hosting.IHostApplicationBuilder"/>
    /// will add console-based logger, namely <seealso cref="Microsoft.Extensions.Logging.ILoggerFactory"/>
    /// and <seealso cref="Microsoft.Extensions.Logging.ILogger{T}"/>.
    /// They will be removed if <paramref name="keepDefaultLogging"/> is set to <c>false</c>
    /// because stdio is the main channel for MCP traffic.
    /// <br/>
    /// If you still want to keep them, set <paramref name="keepDefaultLogging"/> to <c>true</c>.
    /// </remarks>
    public static IServiceCollection AddMcpServer(
        this IServiceCollection services,
        Implementation serverInfo,
        Action<McpServerBuilder> configure,
        bool keepDefaultLogging = false
    )
    {
        if (!keepDefaultLogging)
        {
            services.Remove(
                services.First(s =>
                    s.ServiceType == typeof(ILoggerFactory)
                    && s.ImplementationType == typeof(LoggerFactory)
                )
            );
            services.Remove(
                services.First(s =>
                    s.ServiceType == typeof(ILogger<>) && s.ImplementationType == typeof(Logger<>)
                )
            );
        }

        // Create server builder with host's service collection
        var serverBuilder = new McpServerBuilder(serverInfo, services);

        // Apply configuration if provided
        configure?.Invoke(serverBuilder);

        // Add hosted service
        services.AddSingleton(serverBuilder.Build());
        services.AddHostedService<McpServerHostedService>();

        return services;
    }
}

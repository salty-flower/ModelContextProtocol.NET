using System;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server;

/// <summary>
/// Interface for an MCP server.
/// </summary>
public interface IMcpServer : IAsyncDisposable
{
    /// <summary>
    /// Gets the server implementation information.
    /// </summary>
    Implementation ServerInfo { get; }

    /// <summary>
    /// Starts the server.
    /// </summary>
    void Start(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the server.
    /// </summary>
    void Stop(CancellationToken cancellationToken = default);

    /// <summary>
    /// Runs the server and blocks until the cancellation token is triggered.
    /// This is a convenience method that combines Start(), waiting for cancellation, and Stop().
    /// </summary>
    /// <param name="cancellationToken">Optional token to cancel server execution</param>
    Task RunAsync(CancellationToken cancellationToken = default);
}

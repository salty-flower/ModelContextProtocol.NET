using System;
using System.Threading;
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
}

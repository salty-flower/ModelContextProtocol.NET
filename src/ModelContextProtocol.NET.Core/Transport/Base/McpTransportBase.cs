using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Transport.Base;

/// <summary>
/// Base class for all MCP transport implementations.
/// Provides common functionality for message handling and logging.
/// </summary>
public abstract class McpTransportBase<T>(ILogger<T> logger) : IMcpTransportBase
    where T : McpTransportBase<T>
{
    protected readonly ILogger<T> Logger = logger;
    protected bool isDisposed;

    /// <summary>
    /// Establishes the transport connection.
    /// </summary>
    public abstract Task ConnectAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a single message from the transport.
    /// </summary>
    /// <returns>The message read, or null if the transport is closed.</returns>
    public abstract Task<JsonRpcMessage?> ReadMessageAsync(
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Writes a single message to the transport.
    /// </summary>
    public abstract Task WriteMessageAsync(
        JsonRpcMessage message,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Disposes of the transport resources.
    /// </summary>
    public abstract ValueTask DisposeAsync();

    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
    }

    protected void SetDisposed()
    {
        isDisposed = true;
    }
}

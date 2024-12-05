using System;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Transport.Base;

// Non-generic interface for MCP transport implementations.
public interface IMcpTransportBase : IAsyncDisposable
{
    Task ConnectAsync(CancellationToken cancellationToken = default);
    Task<JsonRpcMessage?> ReadMessageAsync(CancellationToken cancellationToken = default);
    Task WriteMessageAsync(JsonRpcMessage message, CancellationToken cancellationToken = default);
}

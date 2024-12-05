using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public interface IJsonRpcResponse
{
    public RpcId Id { get; }
}

using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public interface IJsonRpcRequest
{
    public RpcId Id { get; }

    public string Method { get; }
}

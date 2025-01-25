using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public class JsonRpcError : JsonRpcMessage
{
    public required RpcId Id { get; init; }

    public required JsonRpcErrorData Error { get; init; }
}

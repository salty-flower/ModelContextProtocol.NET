using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public class JsonRpcResponse<TResult> : JsonRpcMessage, IJsonRpcResponse
{
    public required RpcId Id { get; init; }

    public required TResult Result { get; init; }
}

using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public abstract class JsonRpcRequest<TParams> : JsonRpcMessage, IJsonRpcRequest
{
    public required RpcId Id { get; init; }

    public abstract string Method { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TParams? Params { get; init; }
}

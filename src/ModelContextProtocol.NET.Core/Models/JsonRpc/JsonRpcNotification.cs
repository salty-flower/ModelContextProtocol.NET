using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public abstract class JsonRpcNotification<TParams> : JsonRpcMessage, IJsonRpcNotification
{
    public abstract string Method { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TParams? Params { get; init; }
}

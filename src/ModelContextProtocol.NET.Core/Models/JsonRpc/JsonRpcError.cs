using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Json;
using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public class JsonRpcError : JsonRpcMessage
{
    [JsonConverter(typeof(RpcIdJsonConverter))]
    public required RpcId Id { get; init; }

    public required JsonRpcErrorData Error { get; init; }
}

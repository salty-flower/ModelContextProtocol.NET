using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public class JsonRpcErrorData
{
    public required int Code { get; init; }

    public required string Message { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; init; }
}

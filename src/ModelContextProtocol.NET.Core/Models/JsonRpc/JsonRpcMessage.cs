using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc;

public abstract class JsonRpcMessage
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc { get; } = "2.0";
}

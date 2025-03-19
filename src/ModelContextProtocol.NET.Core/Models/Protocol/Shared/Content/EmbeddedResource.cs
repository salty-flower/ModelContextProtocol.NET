using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record EmbeddedResource : Annotated
{
    [JsonIgnore]
    public string Type => "resource";
    public required ResourceContents Resource { get; init; }
}

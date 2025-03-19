using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record TextContent : Annotated
{
    [JsonIgnore]
    public string Type => "text";
    public required string Text { get; init; }

    public static implicit operator Annotated[](TextContent textContent) => [textContent];

    public static implicit operator TextContent(string text) => new TextContent { Text = text };
}

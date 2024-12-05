namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record TextContent : Annotated
{
    public string Type => "text";
    public required string Text { get; init; }
}

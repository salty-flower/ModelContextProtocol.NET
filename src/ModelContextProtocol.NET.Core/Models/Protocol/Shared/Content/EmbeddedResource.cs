namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record EmbeddedResource : Annotated
{
    public string Type => "resource";
    public required ResourceContents Resource { get; init; }
}

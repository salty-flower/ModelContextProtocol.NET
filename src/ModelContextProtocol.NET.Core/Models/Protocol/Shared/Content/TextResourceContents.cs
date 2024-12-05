namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record TextResourceContents : ResourceContents
{
    public required string Text { get; init; }
}

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record ResourceContents
{
    public required string Uri { get; init; }
    public string? MimeType { get; init; }
}

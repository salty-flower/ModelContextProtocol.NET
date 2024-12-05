namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record BlobResourceContents : ResourceContents
{
    public required string Blob { get; init; }
}

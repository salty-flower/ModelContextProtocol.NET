namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record ImageContent : Annotated
{
    // "type" discriminator emitted automatically via JsonPolymorphic attribute on base record
    public required string Data { get; init; }
    public required string MimeType { get; init; }
}

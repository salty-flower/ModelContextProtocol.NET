namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record EmbeddedResource : Annotated
{
    // "type" discriminator emitted automatically via JsonPolymorphic attribute on base record
    public required ResourceContents Resource { get; init; }
}

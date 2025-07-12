namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record TextContent : Annotated
{
    // The "type" discriminator is emitted automatically by System.Text.Json thanks to
    // the JsonPolymorphic/JsonDerivedType attributes declared on the base record
    // Annotated. An explicit property would clash with the metadata property and
    // break serialization, so it has been removed.
    public required string Text { get; init; }

    public static implicit operator Annotated[](TextContent textContent) => [textContent];

    public static implicit operator TextContent(string text) => new TextContent { Text = text };
}

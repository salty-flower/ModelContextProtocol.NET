using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TextContent), "text")]
[JsonDerivedType(typeof(ImageContent), "image")]
[JsonDerivedType(typeof(EmbeddedResource), "resource")]
public record Annotated
{
    public Annotations? Annotations { get; init; }
}

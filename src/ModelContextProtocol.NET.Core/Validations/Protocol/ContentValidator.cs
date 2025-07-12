using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ContentValidator : AbstractValidator<Annotated>
{
    public ContentValidator(bool allowEmbeddedResource = true)
    {
        RuleFor(x => x)
            .Must(content =>
                content switch
                {
                    TextContent => true,
                    ImageContent => true,
                    EmbeddedResource when allowEmbeddedResource => true,
                    _ => false,
                }
            )
            .WithMessage(content =>
                $"Invalid content type: {content.GetType().Name}. "
                + $"Expected {(allowEmbeddedResource ? "TextContent, ImageContent, or EmbeddedResource" : "TextContent or ImageContent")}"
            );
    }
}

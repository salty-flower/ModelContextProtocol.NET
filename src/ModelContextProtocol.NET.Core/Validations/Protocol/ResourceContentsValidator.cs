using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ResourceContentsValidator : AbstractValidator<ResourceContents>
{
    public ResourceContentsValidator()
    {
        RuleFor(x => x.Uri).NotEmpty();
        RuleFor(x => x)
            .Must(content =>
                content switch
                {
                    TextResourceContents text => !string.IsNullOrEmpty(text.Text),
                    BlobResourceContents blob => !string.IsNullOrEmpty(blob.Blob),
                    _ => false,
                }
            )
            .WithMessage(
                "Content must be either TextResourceContents or BlobResourceContents with non-empty content"
            );
    }
}

using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ResourceTemplateValidator : AbstractValidator<ResourceTemplate>
{
    public ResourceTemplateValidator()
    {
        RuleFor(x => x.UriTemplate).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

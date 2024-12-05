using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class PromptMessageValidator : AbstractValidator<PromptMessage>
{
    public PromptMessageValidator()
    {
        RuleFor(x => x.Content).SetValidator(new ContentValidator());
    }
}

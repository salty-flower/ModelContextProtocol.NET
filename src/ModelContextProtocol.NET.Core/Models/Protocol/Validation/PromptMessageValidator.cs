using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Validation;

public class PromptMessageValidator : AbstractValidator<PromptMessage>
{
    public PromptMessageValidator()
    {
        RuleFor(x => x.Content).SetValidator(new ContentValidator());
    }
}

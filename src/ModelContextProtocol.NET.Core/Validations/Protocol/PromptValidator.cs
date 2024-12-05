using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class PromptValidator : AbstractValidator<Prompt>
{
    public PromptValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        When(
            x => x.Arguments != null,
            () =>
            {
                RuleForEach(x => x.Arguments).SetValidator(new PromptArgumentValidator());
            }
        );
    }
}

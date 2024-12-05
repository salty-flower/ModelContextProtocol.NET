using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class PromptArgumentValidator : AbstractValidator<PromptArgument>
{
    public PromptArgumentValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

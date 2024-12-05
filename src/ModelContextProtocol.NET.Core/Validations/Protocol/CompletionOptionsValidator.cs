using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class CompletionOptionsValidator : AbstractValidator<CompleteResult.CompletionT>
{
    public CompletionOptionsValidator()
    {
        RuleFor(x => x.Values).NotNull();
        RuleFor(x => x.Values.Length)
            .LessThanOrEqualTo(100)
            .WithMessage("Values must not exceed 100 items");
    }
}

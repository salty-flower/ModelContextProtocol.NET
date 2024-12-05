using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class CompleteResultValidator : AbstractValidator<CompleteResult>
{
    public CompleteResultValidator()
    {
        RuleFor(x => x.Completion).NotNull().SetValidator(new CompletionOptionsValidator());
    }
}

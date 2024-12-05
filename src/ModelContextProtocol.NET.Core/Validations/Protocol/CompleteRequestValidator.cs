using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class CompleteRequestValidator : AbstractValidator<CompleteRequest>
{
    public CompleteRequestValidator()
    {
        RuleFor(x => x.Params).NotNull();
        RuleFor(x => x.Params!.Ref).NotNull().SetValidator(new CompletionReferenceValidator());
        RuleFor(x => x.Params!.Argument).NotNull().SetValidator(new CompletionArgumentValidator());
    }
}

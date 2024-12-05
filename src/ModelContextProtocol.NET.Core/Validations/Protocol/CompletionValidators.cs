using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class CompletionReferenceValidator : AbstractValidator<CompleteRequest.Parameters.Reference>
{
    public CompletionReferenceValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(x => x is "ref/resource" or "ref/prompt")
            .WithMessage("Type must be either 'ref/resource' or 'ref/prompt'");

        When(
            x => x.Type == "ref/resource",
            () => RuleFor(x => ((CompleteRequest.Parameters.Reference.Resource)x).Uri).NotEmpty()
        );

        When(
            x => x.Type == "ref/prompt",
            () => RuleFor(x => ((CompleteRequest.Parameters.Reference.Prompt)x).Name).NotEmpty()
        );
    }
}

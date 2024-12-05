using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class CompletionArgumentValidator : AbstractValidator<CompleteRequest.Parameters.ArgumentT>
{
    public CompletionArgumentValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Value).NotNull();
    }
}

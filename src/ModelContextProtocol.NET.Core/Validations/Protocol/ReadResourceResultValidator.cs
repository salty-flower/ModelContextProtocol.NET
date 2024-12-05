using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ReadResourceResultValidator : AbstractValidator<ReadResourceResult>
{
    public ReadResourceResultValidator()
    {
        RuleFor(x => x.Contents).NotEmpty();
        RuleForEach(x => x.Contents).SetValidator(new ResourceContentsValidator());
    }
}

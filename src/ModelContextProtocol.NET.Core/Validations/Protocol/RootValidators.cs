using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class RootValidator : AbstractValidator<Root>
{
    public RootValidator()
    {
        RuleFor(x => x.Uri)
            .NotEmpty()
            .Must(uri => uri.StartsWith("file://"))
            .WithMessage("URI must start with 'file://'");
    }
}

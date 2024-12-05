using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class SetLevelRequestValidator : AbstractValidator<SetLevelRequest>
{
    public SetLevelRequestValidator()
    {
        RuleFor(x => x.Params).NotNull();
        RuleFor(x => x.Params!.Level).NotEmpty();
    }
}

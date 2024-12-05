using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class UnsubscribeRequestValidator : AbstractValidator<UnsubscribeRequest>
{
    public UnsubscribeRequestValidator()
    {
        RuleFor(x => x.Params).NotNull();
        RuleFor(x => x.Params!.Uri).NotEmpty();
    }
}

using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ReadResourceRequestValidator : AbstractValidator<ReadResourceRequest>
{
    public ReadResourceRequestValidator()
    {
        RuleFor(x => x.Params).NotNull();
        RuleFor(x => x.Params!.Uri).NotEmpty();
    }
}

using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class CallToolRequestValidator : AbstractValidator<CallToolRequest>
{
    public CallToolRequestValidator()
    {
        RuleFor(x => x.Params).NotNull();
        RuleFor(x => x.Params!.Name).NotEmpty();
    }
}

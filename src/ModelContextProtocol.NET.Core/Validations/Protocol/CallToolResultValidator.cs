using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class CallToolResultValidator : AbstractValidator<CallToolResult>
{
    public CallToolResultValidator()
    {
        RuleForEach(x => x.Content).SetValidator(new ContentValidator());
    }
}

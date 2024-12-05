using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Validation;

public class SamplingMessageValidator : AbstractValidator<SamplingMessage>
{
    public SamplingMessageValidator()
    {
        RuleFor(x => x.Content).SetValidator(new ContentValidator(allowEmbeddedResource: false));
    }
}

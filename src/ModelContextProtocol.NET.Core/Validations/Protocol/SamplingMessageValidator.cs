using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class SamplingMessageValidator : AbstractValidator<SamplingMessage>
{
    public SamplingMessageValidator()
    {
        RuleFor(x => x.Content).SetValidator(new ContentValidator(allowEmbeddedResource: false));
    }
}

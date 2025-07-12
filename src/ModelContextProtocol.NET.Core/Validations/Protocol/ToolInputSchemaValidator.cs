using System.Linq;
using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ToolInputSchemaValidator : AbstractValidator<ToolInputSchema>
{
    public ToolInputSchemaValidator()
    {
        When(
            x => x.Properties != null || (x.Required != null && x.Required.Count > 0),
            () =>
            {
                RuleFor(x => x.Type)
                    .Equal("object")
                    .WithMessage(
                        "Type must be 'object' when Properties or Required are specified."
                    );
            }
        );

        When(
            x => x.Required != null,
            () =>
            {
                RuleFor(x => x.Properties)
                    .NotNull()
                    .WithMessage("Properties must be specified when Required is specified");
                RuleForEach(x => x.Required)
                    .Must((schema, required) => schema.Properties?.ContainsKey(required) == true)
                    .WithMessage(
                        (_, required) =>
                            $"Required property '{required}' must be defined in Properties"
                    );
            }
        );
    }
}

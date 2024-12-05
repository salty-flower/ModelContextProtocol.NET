using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ModelPreferencesValidator : AbstractValidator<ModelPreferences>
{
    public ModelPreferencesValidator()
    {
        When(
            x => x.CostPriority.HasValue,
            () =>
            {
                RuleFor(x => x.CostPriority!.Value)
                    .InclusiveBetween(0, 1)
                    .WithMessage("Cost priority must be between 0 and 1");
            }
        );

        When(
            x => x.SpeedPriority.HasValue,
            () =>
            {
                RuleFor(x => x.SpeedPriority!.Value)
                    .InclusiveBetween(0, 1)
                    .WithMessage("Speed priority must be between 0 and 1");
            }
        );

        When(
            x => x.IntelligencePriority.HasValue,
            () =>
            {
                RuleFor(x => x.IntelligencePriority!.Value)
                    .InclusiveBetween(0, 1)
                    .WithMessage("Intelligence priority must be between 0 and 1");
            }
        );
    }
}

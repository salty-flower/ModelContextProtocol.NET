using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ToolValidator : AbstractValidator<Tool>
{
    public ToolValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.InputSchema).NotNull().SetValidator(new ToolInputSchemaValidator());
        RuleFor(x => x.OutputSchema).SetValidator(new ToolInputSchemaValidator());
    }
}

using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public abstract class PaginatedResultValidator<T> : AbstractValidator<T>
    where T : IPaginatedResult
{
    protected PaginatedResultValidator()
    {
        When(
            x => x.NextCursor != null,
            () =>
            {
                RuleFor(x => x.NextCursor).NotEmpty();
            }
        );
    }
}

using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public abstract class PaginatedRequestParamsValidator<T> : AbstractValidator<T>
    where T : IPaginatedRequestParams
{
    protected PaginatedRequestParamsValidator()
    {
        When(
            x => x.Cursor != null,
            () =>
            {
                RuleFor(x => x.Cursor).NotEmpty();
            }
        );
    }
}

using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ListResourcesResultValidator : PaginatedResultValidator<ListResourcesResult>
{
    public ListResourcesResultValidator()
    {
        RuleFor(x => x.Resources).NotNull();
        RuleForEach(x => x.Resources).SetValidator(new ResourceValidator());
    }
}

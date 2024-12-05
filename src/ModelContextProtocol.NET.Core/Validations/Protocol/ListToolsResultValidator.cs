using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ListToolsResultValidator : PaginatedResultValidator<ListToolsResult>
{
    public ListToolsResultValidator()
    {
        RuleFor(x => x.Tools).NotNull();
        RuleForEach(x => x.Tools).SetValidator(new ToolValidator());
    }
}

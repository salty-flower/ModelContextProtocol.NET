using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ListResourceTemplatesResultValidator
    : PaginatedResultValidator<ListResourceTemplatesResult>
{
    public ListResourceTemplatesResultValidator()
    {
        RuleFor(x => x.ResourceTemplates).NotNull();
        RuleForEach(x => x.ResourceTemplates).SetValidator(new ResourceTemplateValidator());
    }
}

using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class ListRootsResultValidator : AbstractValidator<ListRootsResult>
{
    public ListRootsResultValidator()
    {
        RuleFor(x => x.Roots).NotNull();
        RuleForEach(x => x.Roots).SetValidator(new RootValidator());
    }
}

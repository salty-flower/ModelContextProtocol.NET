using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.NET.Server.Features.Prompts;

namespace ModelContextProtocol.NET.Server.Builder;

internal class PromptRegistry(IServiceCollection services) : IPromptRegistry
{
    public IPromptRegistry AddHandler<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler
    >()
        where THandler : class, IPromptHandler
    {
        services.AddTransient<IPromptHandler, THandler>();
        return this;
    }
}

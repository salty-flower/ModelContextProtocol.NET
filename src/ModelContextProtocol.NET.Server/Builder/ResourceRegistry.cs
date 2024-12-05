using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.NET.Server.Features.Resources;

namespace ModelContextProtocol.NET.Server.Builder;

internal class ResourceRegistry(IServiceCollection services) : IResourceRegistry
{
    public IResourceRegistry AddHandler<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler
    >()
        where THandler : class, IResourceHandler
    {
        services.AddTransient<IResourceHandler, THandler>();
        return this;
    }
}

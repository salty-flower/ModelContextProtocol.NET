using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.NET.Server.Features.Tools;

namespace ModelContextProtocol.NET.Server.Builder;

internal class ToolRegistry(IServiceCollection services) : IToolRegistry
{
    public IToolRegistry AddHandler<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler
    >()
        where THandler : class, IToolHandler
    {
        services.AddTransient<IToolHandler, THandler>();
        return this;
    }
}

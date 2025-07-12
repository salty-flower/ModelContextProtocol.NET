using System.Diagnostics.CodeAnalysis;
using ModelContextProtocol.NET.Server.Features.Resources;

namespace ModelContextProtocol.NET.Server.Builder;

/// <summary>
/// Registry for configuring resources.
/// </summary>
public interface IResourceRegistry
{
    /// <summary>
    /// Adds a resource handler.
    /// </summary>
    IResourceRegistry AddHandler<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler
    >()
        where THandler : class, IResourceHandler;
}

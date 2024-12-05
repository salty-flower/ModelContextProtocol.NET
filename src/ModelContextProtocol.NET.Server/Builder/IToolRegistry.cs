using System.Diagnostics.CodeAnalysis;
using ModelContextProtocol.NET.Server.Features.Tools;

namespace ModelContextProtocol.NET.Server.Builder;

/// <summary>
/// Registry for configuring tools.
/// </summary>

public interface IToolRegistry
{
    /// <summary>
    /// Adds a tool handler.
    /// </summary>
    IToolRegistry AddHandler<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler
    >()
        where THandler : class, IToolHandler;
}

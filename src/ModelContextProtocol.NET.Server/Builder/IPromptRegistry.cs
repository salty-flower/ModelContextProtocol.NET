using System.Diagnostics.CodeAnalysis;
using ModelContextProtocol.NET.Server.Features.Prompts;

namespace ModelContextProtocol.NET.Server.Builder;

/// <summary>
/// Registry for configuring prompts.
/// </summary>
public interface IPromptRegistry
{
    /// <summary>
    /// Adds a prompt handler.
    /// </summary>
    IPromptRegistry AddHandler<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler
    >()
        where THandler : class, IPromptHandler;
}

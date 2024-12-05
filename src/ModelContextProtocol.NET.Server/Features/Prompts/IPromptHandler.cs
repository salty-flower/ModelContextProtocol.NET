using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Features.Prompts;

/// <summary>
/// Base interface for prompt handlers.
/// </summary>
public interface IPromptHandler
{
    /// <summary>
    /// Prompt template information including name, description, and arguments.
    /// </summary>
    Prompt Template { get; }

    /// <summary>
    /// Handles a prompt request.
    /// </summary>
    Task<IReadOnlyList<PromptMessage>> HandleAsync(
        object arguments,
        CancellationToken cancellationToken = default
    );
}

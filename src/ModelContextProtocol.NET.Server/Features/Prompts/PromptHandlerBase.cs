using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Server.Contexts;

namespace ModelContextProtocol.NET.Server.Features.Prompts;

/// <summary>
/// Base class for implementing prompt handlers with strongly-typed arguments.
/// </summary>
public abstract class PromptHandlerBase<TArgs>(
    Prompt template,
    IServerContext serverContext,
    ISessionContext sessionContext
) : IPromptHandler
    where TArgs : class
{
    protected Prompt Template { get; init; } = template;
    protected IServerContext ServerContext { get; init; } = serverContext;
    protected ISessionContext SessionContext { get; init; } = sessionContext;

    Prompt IPromptHandler.Template => Template;

    /// <summary>
    /// Handles a prompt request with strongly-typed arguments.
    /// </summary>
    protected abstract Task<IReadOnlyList<PromptMessage>> HandleAsync(
        TArgs arguments,
        CancellationToken cancellationToken = default
    );

    async Task<IReadOnlyList<PromptMessage>> IPromptHandler.HandleAsync(
        object arguments,
        CancellationToken cancellationToken
    )
    {
        if (arguments is not TArgs typedArgs)
        {
            throw new ArgumentException(
                $"Invalid arguments type. Expected {typeof(TArgs).Name}",
                nameof(arguments)
            );
        }

        return await HandleAsync(typedArgs, cancellationToken);
    }
}

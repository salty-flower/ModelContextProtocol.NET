using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;
using ModelContextProtocol.NET.Server.Contexts;

namespace ModelContextProtocol.NET.Server.Features.Resources;

/// <summary>
/// Base class for implementing resource handlers with strongly-typed parameters.
/// </summary>
public abstract class ResourceHandlerBase<TParams> : IResourceHandler
    where TParams : class
{
    protected ResourceTemplate Template { get; }
    protected IServerContext ServerContext { get; }
    protected ISessionContext SessionContext { get; }

    protected ResourceHandlerBase(
        ResourceTemplate template,
        IServerContext serverContext,
        ISessionContext sessionContext
    )
    {
        Template = template;
        ServerContext = serverContext;
        SessionContext = sessionContext;
    }

    ResourceTemplate IResourceHandler.Template => Template;

    /// <summary>
    /// Handles a resource request with strongly-typed parameters.
    /// </summary>
    protected abstract Task<IReadOnlyList<ResourceContents>> HandleAsync(
        TParams parameters,
        CancellationToken cancellationToken = default
    );

    async Task<IReadOnlyList<ResourceContents>> IResourceHandler.HandleAsync(
        object parameters,
        CancellationToken cancellationToken
    )
    {
        if (parameters is not TParams typedParams)
        {
            throw new ArgumentException(
                $"Invalid parameters type. Expected {typeof(TParams).Name}",
                nameof(parameters)
            );
        }

        return await HandleAsync(typedParams, cancellationToken);
    }
}

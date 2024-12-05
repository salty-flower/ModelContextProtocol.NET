using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Server.Features.Resources;

/// <summary>
/// Base interface for resource handlers.
/// </summary>
public interface IResourceHandler
{
    /// <summary>
    /// Resource template information including URI pattern and metadata.
    /// </summary>
    ResourceTemplate Template { get; }

    /// <summary>
    /// Handles a resource request.
    /// </summary>
    Task<IReadOnlyList<ResourceContents>> HandleAsync(
        object parameters,
        CancellationToken cancellationToken = default
    );
}

using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Features.Tools;

/// <summary>
/// Base interface for tool handlers.
/// </summary>
public interface IToolHandler
{
    /// <summary>
    /// Tool information including name, description, and schema.
    /// </summary>
    Tool Tool { get; }

    JsonTypeInfo JsonTypeInfo { get; }

    /// <summary>
    /// Handles a tool invocation.
    /// </summary>
    Task<CallToolResult> HandleAsync(
        object parameters,
        CancellationToken cancellationToken = default
    );
}

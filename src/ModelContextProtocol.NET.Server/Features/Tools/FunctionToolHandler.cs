using System;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Features.Tools;

internal class FunctionToolHandler<TParams>(
    Tool tool,
    JsonTypeInfo jsonTypeInfo,
    Func<TParams, CancellationToken, Task<CallToolResult>> handler
) : ToolHandlerBase<TParams>(tool)
    where TParams : class
{
    public override JsonTypeInfo JsonTypeInfo => jsonTypeInfo;

    protected override Task<CallToolResult> HandleAsync(
        TParams parameters,
        CancellationToken cancellationToken = default
    ) => handler(parameters, cancellationToken);
}

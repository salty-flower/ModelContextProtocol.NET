using System.Collections.Generic;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public record ListToolsResult : ServerResult, IPaginatedResult
{
    public required IReadOnlyList<Tool> Tools { get; init; }
    public string? NextCursor { get; init; }
}

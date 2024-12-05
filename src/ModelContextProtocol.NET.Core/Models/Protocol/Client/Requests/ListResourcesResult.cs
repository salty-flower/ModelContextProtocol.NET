using System.Collections.Generic;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public record ListResourcesResult : ServerResult, IPaginatedResult
{
    public required IReadOnlyList<Resource> Resources { get; init; }
    public string? NextCursor { get; init; }
}

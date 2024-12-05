using System.Collections.Generic;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public record ListResourceTemplatesResult : ServerResult, IPaginatedResult
{
    public required IReadOnlyList<ResourceTemplate> ResourceTemplates { get; init; }
    public string? NextCursor { get; init; }
}

using System.Collections.Generic;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public record ListPromptsResult : ServerResult, IPaginatedResult
{
    public required IReadOnlyList<Prompt> Prompts { get; init; }
    public string? NextCursor { get; init; }
}

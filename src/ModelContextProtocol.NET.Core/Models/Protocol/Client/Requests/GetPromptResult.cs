using System.Collections.Generic;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public record GetPromptResult : ServerResult
{
    public string? Description { get; init; }
    public required IReadOnlyList<PromptMessage> Messages { get; init; }
}

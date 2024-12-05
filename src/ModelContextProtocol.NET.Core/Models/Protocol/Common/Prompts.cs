using System.Collections.Generic;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record Prompt
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public IReadOnlyList<PromptArgument>? Arguments { get; init; }
}

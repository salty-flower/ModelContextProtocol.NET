using System.Collections.Generic;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record Annotations
{
    public IReadOnlyList<Role>? Audience { get; init; }
    public float? Priority { get; init; }
}

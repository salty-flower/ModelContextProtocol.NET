using System.Collections.Generic;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record ModelPreferences
{
    public IReadOnlyList<ModelHint>? Hints { get; init; }
    public float? CostPriority { get; init; }
    public float? SpeedPriority { get; init; }
    public float? IntelligencePriority { get; init; }
}

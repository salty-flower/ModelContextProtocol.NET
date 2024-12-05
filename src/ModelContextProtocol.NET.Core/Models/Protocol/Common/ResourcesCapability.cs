namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record ResourcesCapability
{
    public bool? Subscribe { get; init; }
    public bool? ListChanged { get; init; }
}

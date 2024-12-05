namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record Implementation
{
    public required string Name { get; init; }
    public required string Version { get; init; }
}

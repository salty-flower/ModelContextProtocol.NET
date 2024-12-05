namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record Root
{
    public required string Uri { get; init; }
    public string? Name { get; init; }
}

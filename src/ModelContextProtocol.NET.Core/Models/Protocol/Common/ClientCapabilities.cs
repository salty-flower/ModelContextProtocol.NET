namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record ClientCapabilities : Capabilities
{
    public RootsCapability? Roots { get; init; }

    public object? Sampling { get; init; }
}

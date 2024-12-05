namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record Tool
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required ToolInputSchema InputSchema { get; init; }
}

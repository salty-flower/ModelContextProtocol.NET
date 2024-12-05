namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record PromptArgument
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public bool? Required { get; init; }
}

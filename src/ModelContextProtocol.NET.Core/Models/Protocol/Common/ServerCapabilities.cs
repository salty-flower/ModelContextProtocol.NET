namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record ServerCapabilities : Capabilities
{
    public object? Logging { get; init; }

    public PromptsCapability? Prompts { get; init; }

    public ResourcesCapability? Resources { get; init; }

    public ToolsCapability? Tools { get; init; }
}

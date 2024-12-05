using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;

public record CallToolResult : Result
{
    public required Annotated[] Content { get; init; } // (TextContent | ImageContent | EmbeddedResource)[]
    public bool? IsError { get; init; }
}

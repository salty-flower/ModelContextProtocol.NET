using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;

public record CreateMessageResult : ServerResult, ISamplingMessage
{
    public required string Model { get; init; }
    public string? StopReason { get; init; }
    public required Role Role { get; init; }
    public required Annotated Content { get; init; }
}

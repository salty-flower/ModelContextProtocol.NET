using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;

public record InitializeResult : ServerResult
{
    public required string ProtocolVersion { get; init; }
    public required Implementation ServerInfo { get; init; }
    public required ServerCapabilities Capabilities { get; init; }
    public string? Instructions { get; init; }
}

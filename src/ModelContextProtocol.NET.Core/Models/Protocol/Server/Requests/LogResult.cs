using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;

public record LogResult : ServerResult
{
    public bool Success { get; init; }
}

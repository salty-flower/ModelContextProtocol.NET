using System.Collections.Generic;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;

public record ListRootsResult : ClientResult
{
    public required IReadOnlyList<Root> Roots { get; init; }
}

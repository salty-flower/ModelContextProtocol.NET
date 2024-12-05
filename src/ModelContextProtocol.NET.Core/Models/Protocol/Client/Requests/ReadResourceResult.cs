using System.Collections.Generic;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public record ReadResourceResult : ServerResult
{
    public required IReadOnlyList<ResourceContents> Contents { get; init; }
}

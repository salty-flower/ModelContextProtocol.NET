using System.Collections.Generic;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public abstract record Capabilities
{
    public IDictionary<string, object>? Experimental { get; init; }
}

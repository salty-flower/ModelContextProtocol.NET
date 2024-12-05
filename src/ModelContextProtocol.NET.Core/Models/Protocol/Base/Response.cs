using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Base;

[JsonPolymorphic]
public abstract record Result
{
    [JsonPropertyName("_meta")]
    public Dictionary<string, object>? Metadata { get; init; }
}

public abstract record ServerResult : Result, IServerMessage { }

public abstract record ClientResult : Result, IClientMessage { }

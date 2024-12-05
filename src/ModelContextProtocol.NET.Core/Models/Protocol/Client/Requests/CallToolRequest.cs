using System.Collections.Generic;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class CallToolRequest : ClientRequest<CallToolRequest.Parameters, CallToolRequest.Meta>
{
    public override string Method => "tools/call";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required string Name { get; init; }
        public Dictionary<string, object>? Arguments { get; init; }

        [JsonPropertyName("_meta")]
        public override CallToolRequest.Meta? Metadata { get; init; }
    }
}

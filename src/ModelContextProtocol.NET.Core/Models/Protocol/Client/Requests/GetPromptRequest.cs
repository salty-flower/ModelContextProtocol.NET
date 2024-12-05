using System.Collections.Generic;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class GetPromptRequest : ClientRequest<GetPromptRequest.Parameters, GetPromptRequest.Meta>
{
    public override string Method => "prompts/get";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required string Name { get; init; }
        public Dictionary<string, string>? Arguments { get; init; }

        [JsonPropertyName("_meta")]
        public override GetPromptRequest.Meta? Metadata { get; init; }
    }
}

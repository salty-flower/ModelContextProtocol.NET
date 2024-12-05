using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class ListPromptsRequest
    : ClientRequest<ListPromptsRequest.Parameters, ListPromptsRequest.Meta>
{
    public override string Method => "prompts/list";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>, IPaginatedRequestParams
    {
        public string? Cursor { get; init; }

        [JsonPropertyName("_meta")]
        public override ListPromptsRequest.Meta? Metadata { get; init; }
    }
}

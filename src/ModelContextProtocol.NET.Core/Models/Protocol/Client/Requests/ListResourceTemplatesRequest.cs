using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class ListResourceTemplatesRequest
    : ClientRequest<ListResourceTemplatesRequest.Parameters, ListResourceTemplatesRequest.Meta>
{
    public override string Method => "resources/templates/list";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>, IPaginatedRequestParams
    {
        public string? Cursor { get; init; }

        [JsonPropertyName("_meta")]
        public override ListResourceTemplatesRequest.Meta? Metadata { get; init; }
    }
}

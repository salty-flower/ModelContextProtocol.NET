using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class ListResourcesRequest
    : ClientRequest<ListResourcesRequest.Parameters, ListResourcesRequest.Meta>
{
    public override string Method => "resources/list";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>, IPaginatedRequestParams
    {
        public string? Cursor { get; init; }

        [JsonPropertyName("_meta")]
        public override ListResourcesRequest.Meta? Metadata { get; init; }
    }
}

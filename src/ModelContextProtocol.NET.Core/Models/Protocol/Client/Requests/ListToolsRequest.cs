using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class ListToolsRequest : ClientRequest<ListToolsRequest.Parameters, ListToolsRequest.Meta>
{
    public override string Method => "tools/list";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>, IPaginatedRequestParams
    {
        public string? Cursor { get; init; }

        [JsonPropertyName("_meta")]
        public override ListToolsRequest.Meta? Metadata { get; init; }
    }
}

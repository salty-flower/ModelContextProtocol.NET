using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;

public class ListRootsRequest : ServerRequest<ListRootsRequest.Parameters, ListRootsRequest.Meta>
{
    public override string Method => "roots/list";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        [JsonPropertyName("_meta")]
        public override ListRootsRequest.Meta? Metadata { get; init; }
    }
}

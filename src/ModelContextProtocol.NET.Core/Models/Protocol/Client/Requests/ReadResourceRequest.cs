using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class ReadResourceRequest
    : ClientRequest<ReadResourceRequest.Parameters, ReadResourceRequest.Meta>
{
    public override string Method => "resources/read";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required string Uri { get; init; }

        [JsonPropertyName("_meta")]
        public override ReadResourceRequest.Meta? Metadata { get; init; }
    }
}

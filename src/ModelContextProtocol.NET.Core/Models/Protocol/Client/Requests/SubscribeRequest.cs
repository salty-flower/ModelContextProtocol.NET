using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class SubscribeRequest : ClientRequest<SubscribeRequest.Parameters, SubscribeRequest.Meta>
{
    public override string Method => "resources/subscribe";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required string Uri { get; init; }

        [JsonPropertyName("_meta")]
        public override SubscribeRequest.Meta? Metadata { get; init; }
    }
}

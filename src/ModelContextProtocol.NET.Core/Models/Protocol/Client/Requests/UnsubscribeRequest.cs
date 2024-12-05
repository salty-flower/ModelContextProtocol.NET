using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class UnsubscribeRequest
    : ClientRequest<UnsubscribeRequest.Parameters, UnsubscribeRequest.Meta>
{
    public override string Method => "resources/unsubscribe";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required string Uri { get; init; }

        [JsonPropertyName("_meta")]
        public override UnsubscribeRequest.Meta? Metadata { get; init; }
    }
}

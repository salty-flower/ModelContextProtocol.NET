using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class SetLevelRequest : ClientRequest<SetLevelRequest.Parameters, SetLevelRequest.Meta>
{
    public override string Method => "logging/setLevel";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required string Level { get; init; }

        [JsonPropertyName("_meta")]
        public override SetLevelRequest.Meta? Metadata { get; init; }
    }
}

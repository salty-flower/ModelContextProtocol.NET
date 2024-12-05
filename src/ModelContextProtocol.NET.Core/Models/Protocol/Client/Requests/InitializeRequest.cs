using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class InitializeRequest : ClientRequest<InitializeRequest.Parameters, InitializeRequest.Meta>
{
    public override string Method => "initialize";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required string ProtocolVersion { get; init; }
        public required Implementation ClientInfo { get; init; }
        public required ClientCapabilities Capabilities { get; init; }

        [JsonPropertyName("_meta")]
        public override InitializeRequest.Meta? Metadata { get; init; }
    }
}

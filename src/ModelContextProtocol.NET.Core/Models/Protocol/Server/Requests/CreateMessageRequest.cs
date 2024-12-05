using System.Collections.Generic;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;

public class CreateMessageRequest
    : ServerRequest<CreateMessageRequest.Parameters, CreateMessageRequest.Meta>
{
    public override string Method => "sampling/createMessage";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required IReadOnlyList<SamplingMessage> Messages { get; init; }
        public ModelPreferences? ModelPreferences { get; init; }
        public string? SystemPrompt { get; init; }
        public string? IncludeContext { get; init; }
        public float? Temperature { get; init; }
        public required int MaxTokens { get; init; }
        public IReadOnlyList<string>? StopSequences { get; init; }

        [JsonPropertyName("_meta")]
        public override CreateMessageRequest.Meta? Metadata { get; init; }
    }
}

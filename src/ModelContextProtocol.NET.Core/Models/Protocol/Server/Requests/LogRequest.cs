using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;

public class LogRequest : ServerRequest<LogRequest.Parameters, LogRequest.Meta>
{
    public override string Method => "log";

    public class Parameters : RequestParams<Meta>
    {
        public required string Message { get; init; }
        public string? Level { get; init; }

        [JsonPropertyName("_meta")]
        public override LogRequest.Meta? Metadata { get; init; }
    }

    public class Meta : RequestParams<Meta>.Meta
    {
        public string? Source { get; init; }
        public string? Category { get; init; }
    }
}

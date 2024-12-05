using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public class CompleteRequest : ClientRequest<CompleteRequest.Parameters, CompleteRequest.Meta>
{
    public override string Method => "completion/complete";

    public class Meta : RequestParams<Meta>.Meta { }

    public class Parameters : RequestParams<Meta>
    {
        public required Reference Ref { get; init; }
        public required ArgumentT Argument { get; init; }

        [JsonPropertyName("_meta")]
        public override CompleteRequest.Meta? Metadata { get; init; }

        public class ArgumentT
        {
            public required string Name { get; init; }
            public required string Value { get; init; }
        }

        public abstract record Reference
        {
            protected Reference(string type)
            {
                Type = type;
            } // Prevent external inheritance

            public required string Type { get; init; }

            public sealed record Resource() : Reference("ref/resource")
            {
                public required string Uri { get; init; }
            }

            public sealed record Prompt() : Reference("ref/prompt")
            {
                public required string Name { get; init; }
            }
        }
    }
}

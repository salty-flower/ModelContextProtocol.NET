using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;

public record CompleteResult : ServerResult
{
    public required CompletionT Completion { get; init; }

    public record CompletionT
    {
        public required string[] Values { get; init; }
        public int? Total { get; init; }
        public bool? HasMore { get; init; }
    }
}

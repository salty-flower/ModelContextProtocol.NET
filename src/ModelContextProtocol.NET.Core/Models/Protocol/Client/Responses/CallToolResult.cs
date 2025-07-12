using System.Text.Json;
using System.Text.Json.Nodes;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;

public record CallToolResult : Result
{
    public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
    {
        WriteIndented = true,
        NewLine = "\n",
    };

    public Annotated[]? Content { get; init; }
    public JsonNode? StructuredContent { get; init; }
    public bool? IsError { get; init; }

    public static CallToolResult FromJsonNode(
        JsonNode node,
        bool isError,
        JsonSerializerOptions? jsonSerializerOptions = null
    ) =>
        new()
        {
            Content = (TextContent)
                node.ToJsonString(jsonSerializerOptions ?? DefaultJsonSerializerOptions),
            StructuredContent = node,
            IsError = isError,
        };
}

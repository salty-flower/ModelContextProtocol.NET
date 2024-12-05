using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;
using System.Text.Json.Serialization.Metadata;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Features.Tools;

public static class JsonTypeInfoToolInputSchemaHelper
{
    private static readonly JsonNode ObjectTypeArray = (JsonNode)
        JsonSerializer.Deserialize(
            """["object"]""",
            typeof(JsonNode),
            ToolInputSchemaSerializerContext.Default
        )!;

    public static ToolInputSchema? GetToolSchema<T>(this JsonTypeInfo<T> jsonTypeInfo)
    {
        var schemaNode = jsonTypeInfo.GetJsonSchemaAsNode();
        schemaNode["type"] = ObjectTypeArray.DeepClone();
        return schemaNode.Deserialize(ToolInputSchemaSerializerContext.Default.ToolInputSchema);
    }
}

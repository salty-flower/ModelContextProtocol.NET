using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;
using System.Text.Json.Serialization.Metadata;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Features.Tools;

public static class JsonTypeInfoToolInputSchemaHelper
{
    public static ToolInputSchema? GetToolSchema<T>(this JsonTypeInfo<T> jsonTypeInfo)
    {
        var schemaNode = jsonTypeInfo.GetJsonSchemaAsNode();

        // Normalize the "type" field: if it is an array (e.g. ["object", "null"]),
        // pick the first non-null entry so it can be deserialized into the string-based
        // Type property of ToolInputSchema.
        if (schemaNode["type"] is JsonArray typeArray && typeArray.Count > 0)
        {
            // Prefer the first non-null type, otherwise take the first element.
            var firstNonNull =
                typeArray.FirstOrDefault(t => t?.GetValue<string>() != "null") ?? typeArray[0];
            schemaNode["type"] = JsonValue.Create(firstNonNull?.GetValue<string>());
        }

        return schemaNode.Deserialize(ToolInputSchemaSerializerContext.Default.ToolInputSchema);
    }
}

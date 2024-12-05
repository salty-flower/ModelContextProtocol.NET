using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Features;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(JsonNode))]
[JsonSerializable(typeof(ToolInputSchema))]
internal partial class ToolInputSchemaSerializerContext : JsonSerializerContext { }

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Server.Features;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(JsonElement))]
internal partial class DictSerializerContext : JsonSerializerContext { }

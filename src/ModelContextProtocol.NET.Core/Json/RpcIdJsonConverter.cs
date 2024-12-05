using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Json;

public class RpcIdJsonConverter : JsonConverter<RpcId>
{
    public override RpcId Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return new RpcId();
            case JsonTokenType.String:
                return new RpcId(reader.GetString()!);
            case JsonTokenType.Number:
                var numberAsDouble = reader.GetDouble();
                if (numberAsDouble % 1 != 0)
                    throw new JsonException("JSON-RPC id must not be a fractional number");
                if (numberAsDouble > long.MaxValue || numberAsDouble < long.MinValue)
                    throw new JsonException("JSON-RPC id number is outside the valid range");
                return new RpcId((long)numberAsDouble);
            default:
                throw new JsonException("Invalid JSON-RPC id type");
        }
    }

    public override void Write(Utf8JsonWriter writer, RpcId value, JsonSerializerOptions options)
    {
        if (value.IsNull)
            writer.WriteNullValue();
        else if (value.IsString)
            writer.WriteStringValue(value.AsString);
        else if (value.IsNumber)
            writer.WriteNumberValue(value.AsNumber!.Value);
    }
}

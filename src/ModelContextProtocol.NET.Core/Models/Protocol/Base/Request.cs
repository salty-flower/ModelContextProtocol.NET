using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Base;

public interface IClientMessage { }

public interface IServerMessage { }

public abstract class RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta
{
    [JsonPropertyName("_meta")]
    public abstract TMeta? Metadata { get; init; }

    public class Meta
    {
        [JsonConverter(typeof(ProgressTokenJsonConverter))]
        public string? ProgressToken { get; init; }
    }
}

public abstract class ServerRequest<TParams, TMeta> : Request<TParams, TMeta>, IServerMessage
    where TParams : RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta { }

public abstract class ClientRequest<TParams, TMeta> : Request<TParams, TMeta>, IClientMessage
    where TParams : RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta { }

[JsonPolymorphic]
public abstract class Request<TParams, TMeta> : JsonRpcRequest<TParams>
    where TParams : RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta { }


internal class ProgressTokenJsonConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetString();
            case JsonTokenType.Number:
            {
                var stringValue = reader.GetInt32();
                return stringValue.ToString(CultureInfo.InvariantCulture);
            }
            case JsonTokenType.Null:
            case JsonTokenType.None:
                return null;
            case JsonTokenType.StartObject:
            case JsonTokenType.EndObject:
            case JsonTokenType.StartArray:
            case JsonTokenType.EndArray:
            case JsonTokenType.PropertyName:
            case JsonTokenType.Comment:
            case JsonTokenType.True:
            case JsonTokenType.False:
            default:
                throw new JsonException($"Invalid JSON for ProgressToken (token type: '{reader.TokenType}')");
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
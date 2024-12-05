using System;
using System.Text.Json;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Json;

public static class JsonRpcSerializer
{
    public static JsonRpcMessage ParseMessage(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        ValidateJsonRpcVersion(root);

        bool hasId = root.TryGetProperty("id", out _);
        bool hasMethod = root.TryGetProperty("method", out var methodProperty);
        bool hasResult = root.TryGetProperty("result", out _);
        bool hasError = root.TryGetProperty("error", out _);

        Type messageType = DetermineMessageType(hasId, hasMethod, hasResult, hasError);

        if (hasMethod)
        {
            var method = methodProperty.GetString()!;
            messageType = hasId
                ? MethodTypeResolver.GetRequestType(method)
                : MethodTypeResolver.GetNotificationType(method);
        }

        return (JsonRpcMessage)
            JsonSerializer.Deserialize(json, messageType, McpSerializerContext.Default)!;
    }

    public static string SerializeMessage(JsonRpcMessage message) =>
        JsonSerializer.Serialize(message, message.GetType(), McpSerializerContext.Default);

    private static void ValidateJsonRpcVersion(JsonElement root)
    {
        if (
            !root.TryGetProperty("jsonrpc", out var versionProperty)
            || versionProperty.GetString() != "2.0"
        )
        {
            throw new JsonException("Invalid or missing JSON-RPC version");
        }
    }

    private static Type DetermineMessageType(
        bool hasId,
        bool hasMethod,
        bool hasResult,
        bool hasError
    )
    {
        if (hasId && hasMethod)
            return typeof(JsonRpcRequest<>);
        if (!hasId && hasMethod)
            return typeof(JsonRpcNotification<>);
        if (hasId && hasResult && !hasError)
            return typeof(JsonRpcResponse<>);
        if (hasId && hasError)
            return typeof(JsonRpcError);

        throw new JsonException("Invalid JSON-RPC message structure");
    }
}

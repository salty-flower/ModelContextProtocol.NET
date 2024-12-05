using System;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;
using ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Requests;

namespace ModelContextProtocol.NET.Core.Json;

public static class MethodTypeResolver
{
    public static Type GetRequestType(string method) =>
        string.IsNullOrEmpty(method)
            ? throw new ArgumentException("Method cannot be null or empty", nameof(method))
            : method switch
            {
                // Client Requests
                "initialize" => typeof(InitializeRequest),
                "ping" => typeof(PingRequest),
                "completion/complete" => typeof(CompleteRequest),
                "logging/setLevel" => typeof(SetLevelRequest),
                "prompts/get" => typeof(GetPromptRequest),
                "prompts/list" => typeof(ListPromptsRequest),
                "resources/list" => typeof(ListResourcesRequest),
                "resources/templates/list" => typeof(ListResourceTemplatesRequest),
                "resources/read" => typeof(ReadResourceRequest),
                "resources/subscribe" => typeof(SubscribeRequest),
                "resources/unsubscribe" => typeof(UnsubscribeRequest),
                "tools/list" => typeof(ListToolsRequest),
                "tools/call" => typeof(CallToolRequest),

                // Server Requests
                "sampling/createMessage" => typeof(CreateMessageRequest),
                "log" => typeof(LogRequest),
                "roots/list" => typeof(ListRootsRequest),

                _ => throw new ArgumentException($"Request {method} not found", nameof(method))
            };

    public static Type GetNotificationType(string method) =>
        string.IsNullOrEmpty(method)
            ? throw new ArgumentException("Method cannot be null or empty", nameof(method))
            : method switch
            {
                // Client Notifications
                "notifications/initialized" => typeof(InitializedNotification),
                "notifications/roots/list_changed" => typeof(RootsListChangedNotification),

                // Server Notifications
                "notifications/message" => typeof(LoggingMessageNotification),
                "notifications/resources/list_changed" => typeof(ResourceListChangedNotification),
                "notifications/resources/updated" => typeof(ResourceUpdatedNotification),
                "notifications/tools/list_changed" => typeof(ToolListChangedNotification),
                "notifications/prompts/list_changed" => typeof(PromptListChangedNotification),

                // Shared Notifications (can be sent by both client and server)
                "notifications/cancelled" => typeof(CancelledNotification),
                "notifications/progress" => typeof(ProgressNotification),

                _ => throw new ArgumentException($"Notification {method} not found", nameof(method))
            };
}

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.JsonRpc;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Server.Requests;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Notifications;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Requests;

namespace ModelContextProtocol.NET.Core.Json;

[JsonSerializable(typeof(JsonRpcError))]
//
[JsonSerializable(typeof(Result))]
[JsonSerializable(typeof(ServerResult))]
[JsonSerializable(typeof(IServerMessage))]
[JsonSerializable(typeof(ClientResult))]
[JsonSerializable(typeof(IClientMessage))]
[JsonSerializable(typeof(IPaginatedRequestParams))]
[JsonSerializable(typeof(IPaginatedResult))]
[JsonSerializable(typeof(NotificationParams))]
[JsonSerializable(typeof(NotificationParams.Meta), TypeInfoPropertyName = "NotificationParamsMeta")]
[JsonSerializable(typeof(Capabilities))]
[JsonSerializable(typeof(Implementation))]
[JsonSerializable(typeof(ModelPreferences))]
//
[JsonSerializable(typeof(InitializeRequest))]
[JsonSerializable(
    typeof(InitializeRequest.Parameters),
    TypeInfoPropertyName = "InitializeRequestParameters"
)]
[JsonSerializable(typeof(InitializeRequest.Meta), TypeInfoPropertyName = "InitializeRequestMeta")]
[JsonSerializable(typeof(PingRequest))]
[JsonSerializable(typeof(CompleteRequest))]
[JsonSerializable(
    typeof(CompleteRequest.Parameters),
    TypeInfoPropertyName = "CompleteRequestParameters"
)]
[JsonSerializable(typeof(CompleteRequest.Meta), TypeInfoPropertyName = "CompleteRequestMeta")]
[JsonSerializable(typeof(SetLevelRequest))]
[JsonSerializable(
    typeof(SetLevelRequest.Parameters),
    TypeInfoPropertyName = "SetLevelRequestParameters"
)]
[JsonSerializable(typeof(SetLevelRequest.Meta), TypeInfoPropertyName = "SetLevelRequestMeta")]
[JsonSerializable(typeof(GetPromptRequest))]
[JsonSerializable(
    typeof(GetPromptRequest.Parameters),
    TypeInfoPropertyName = "GetPromptRequestParameters"
)]
[JsonSerializable(typeof(GetPromptRequest.Meta), TypeInfoPropertyName = "GetPromptRequestMeta")]
[JsonSerializable(typeof(ListPromptsRequest))]
[JsonSerializable(
    typeof(ListPromptsRequest.Parameters),
    TypeInfoPropertyName = "ListPromptsRequestParameters"
)]
[JsonSerializable(typeof(ListPromptsRequest.Meta), TypeInfoPropertyName = "ListPromptsRequestMeta")]
[JsonSerializable(typeof(ListResourcesRequest))]
[JsonSerializable(
    typeof(ListResourcesRequest.Parameters),
    TypeInfoPropertyName = "ListResourcesRequestParameters"
)]
[JsonSerializable(
    typeof(ListResourcesRequest.Meta),
    TypeInfoPropertyName = "ListResourcesRequestMeta"
)]
[JsonSerializable(typeof(ListResourceTemplatesRequest))]
[JsonSerializable(
    typeof(ListResourceTemplatesRequest.Parameters),
    TypeInfoPropertyName = "ListResourceTemplatesRequestParameters"
)]
[JsonSerializable(
    typeof(ListResourceTemplatesRequest.Meta),
    TypeInfoPropertyName = "ListResourceTemplatesRequestMeta"
)]
[JsonSerializable(typeof(ReadResourceRequest))]
[JsonSerializable(
    typeof(ReadResourceRequest.Parameters),
    TypeInfoPropertyName = "ReadResourceRequestParameters"
)]
[JsonSerializable(
    typeof(ReadResourceRequest.Meta),
    TypeInfoPropertyName = "ReadResourceRequestMeta"
)]
[JsonSerializable(typeof(SubscribeRequest))]
[JsonSerializable(
    typeof(SubscribeRequest.Parameters),
    TypeInfoPropertyName = "SubscribeRequestParameters"
)]
[JsonSerializable(typeof(SubscribeRequest.Meta), TypeInfoPropertyName = "SubscribeRequestMeta")]
[JsonSerializable(typeof(UnsubscribeRequest))]
[JsonSerializable(
    typeof(UnsubscribeRequest.Parameters),
    TypeInfoPropertyName = "UnsubscribeRequestParameters"
)]
[JsonSerializable(typeof(UnsubscribeRequest.Meta), TypeInfoPropertyName = "UnsubscribeRequestMeta")]
[JsonSerializable(typeof(ListToolsRequest))]
[JsonSerializable(
    typeof(ListToolsRequest.Parameters),
    TypeInfoPropertyName = "ListToolsRequestParameters"
)]
[JsonSerializable(typeof(ListToolsRequest.Meta), TypeInfoPropertyName = "ListToolsRequestMeta")]
[JsonSerializable(typeof(CallToolRequest))]
[JsonSerializable(
    typeof(CallToolRequest.Parameters),
    TypeInfoPropertyName = "CallToolRequestParameters"
)]
[JsonSerializable(typeof(CallToolRequest.Meta), TypeInfoPropertyName = "CallToolRequestMeta")]
[JsonSerializable(typeof(CreateMessageRequest))]
[JsonSerializable(
    typeof(CreateMessageRequest.Parameters),
    TypeInfoPropertyName = "CreateMessageRequestParameters"
)]
[JsonSerializable(
    typeof(CreateMessageRequest.Meta),
    TypeInfoPropertyName = "CreateMessageRequestMeta"
)]
[JsonSerializable(typeof(LogRequest))]
[JsonSerializable(typeof(LogRequest.Parameters), TypeInfoPropertyName = "LogRequestParameters")]
[JsonSerializable(typeof(LogRequest.Meta), TypeInfoPropertyName = "LogRequestMeta")]
[JsonSerializable(typeof(ListRootsRequest))]
[JsonSerializable(
    typeof(ListRootsRequest.Parameters),
    TypeInfoPropertyName = "ListRootsRequestParameters"
)]
[JsonSerializable(typeof(ListRootsRequest.Meta), TypeInfoPropertyName = "ListRootsRequestMeta")]
[JsonSerializable(typeof(InitializedNotification))]
[JsonSerializable(typeof(RootsListChangedNotification))]
[JsonSerializable(typeof(LoggingMessageNotification))]
[JsonSerializable(
    typeof(LoggingMessageNotification.Parameters),
    TypeInfoPropertyName = "LoggingMessageNotificationParameters"
)]
[JsonSerializable(typeof(ResourceListChangedNotification))]
[JsonSerializable(typeof(ResourceUpdatedNotification))]
[JsonSerializable(
    typeof(ResourceUpdatedNotification.Parameters),
    TypeInfoPropertyName = "ResourceUpdatedNotificationParameters"
)]
[JsonSerializable(typeof(ToolListChangedNotification))]
[JsonSerializable(typeof(PromptListChangedNotification))]
[JsonSerializable(typeof(CancelledNotification))]
[JsonSerializable(
    typeof(CancelledNotification.Parameters),
    TypeInfoPropertyName = "CancelledNotificationParameters"
)]
[JsonSerializable(typeof(ProgressNotification))]
[JsonSerializable(
    typeof(ProgressNotification.Parameters),
    TypeInfoPropertyName = "ProgressNotificationParameters"
)]
//
[JsonSerializable(typeof(ServerResult))]
[JsonSerializable(typeof(InitializeResult))]
[JsonSerializable(typeof(CallToolResult))]
[JsonSerializable(typeof(CompleteResult))]
[JsonSerializable(typeof(ListRootsResult))]
[JsonSerializable(typeof(ListPromptsResult))]
[JsonSerializable(typeof(ListResourcesResult))]
[JsonSerializable(typeof(ListResourceTemplatesResult))]
[JsonSerializable(typeof(ReadResourceResult))]
[JsonSerializable(typeof(ListToolsResult))]
//
[JsonSerializable(typeof(JsonRpcResponse<InitializeResult>))]
[JsonSerializable(typeof(JsonRpcResponse<CallToolResult>))]
[JsonSerializable(typeof(JsonRpcResponse<CompleteResult>))]
[JsonSerializable(typeof(JsonRpcResponse<ListRootsResult>))]
[JsonSerializable(typeof(JsonRpcResponse<ListPromptsResult>))]
[JsonSerializable(typeof(JsonRpcResponse<ListResourcesResult>))]
[JsonSerializable(typeof(JsonRpcResponse<ListResourceTemplatesResult>))]
[JsonSerializable(typeof(JsonRpcResponse<ReadResourceResult>))]
[JsonSerializable(typeof(JsonRpcResponse<ListToolsResult>))]
//
[JsonSerializable(typeof(TextContent))]
[JsonSerializable(typeof(ImageContent))]
[JsonSerializable(typeof(ResourceContents))]
[JsonSerializable(typeof(Annotated))]
[JsonSerializable(typeof(Annotations))]
[JsonSerializable(typeof(EmbeddedResource))]
[JsonSerializable(typeof(TextResourceContents))]
[JsonSerializable(typeof(BlobResourceContents))]
[JsonSerializable(typeof(IReadOnlyList<Annotated>))]
[JsonSerializable(typeof(Role))]
//
[JsonSerializable(typeof(JsonElement))]
[JsonSourceGenerationOptions(
    WriteIndented = false,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
public partial class McpSerializerContext : JsonSerializerContext { }

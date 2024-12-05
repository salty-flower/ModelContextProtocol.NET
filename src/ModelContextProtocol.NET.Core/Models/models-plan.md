ModelContextProtocol.NET.Core.Models/
├── JsonRpc/                   # Base JSON-RPC message types
│   ├── JsonRpcMessage.cs      # Base abstract type
│   ├── JsonRpcRequest.cs      # Request with id
│   ├── JsonRpcResponse.cs     # Response with id and result/error
│   ├── JsonRpcNotification.cs # Notification without id
│   ├── JsonRpcError.cs        # Error structure
│   └── Types/                 # Support types
│       ├── RequestId.cs       # string|number ID type
│       └── ErrorData.cs       # Error code+message structure
│
├── Protocol/                  # MCP protocol-specific messages
│   ├── Base/                  # Base types for MCP messages
│   │   ├── Request.cs        # Base for all requests
│   │   ├── Response.cs       # Base for all responses
│   │   └── Notification.cs   # Base for all notifications
│   │
│   ├── Client/               # Client-specific messages
│   │   ├── Requests/        # e.g. InitializeRequest
│   │   ├── Responses/       # e.g. Empty/CreateMessageResult
│   │   └── Notifications/   # e.g. InitializedNotification
│   │
│   ├── Server/               # Server-specific messages
│   │   ├── Requests/        # e.g. CreateMessageRequest
│   │   ├── Responses/       # e.g. InitializeResult
│   │   └── Notifications/   # e.g. LoggingMessageNotification
│   │
│   ├── Shared/              # Messages that can go both ways
│   |   ├── Requests/        # e.g. PingRequest
│   |   ├── Responses/       # Shared response types
│   |   └── Notifications/   # e.g. ProgressNotification
│   |   └── Content/         # Content type models
│   |
│   └── Common/              # building-block types, used as fields/properties in messages
|                            # including capabilities, errors, etc.
│ 
└── Features/                 # Feature-specific models
    ├── Tools/               # Tool-related models
    ├── Resources/           # Resource-related models
    ├── Prompts/            # Prompt-related models
    └── Common/             # Shared feature models like content types
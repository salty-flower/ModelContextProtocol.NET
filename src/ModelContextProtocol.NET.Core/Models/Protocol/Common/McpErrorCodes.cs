namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public static class McpErrorCodes
{
    public const int ParseError = -32700;
    public const int InvalidRequest = -32600;
    public const int MethodNotFound = -32601;
    public const int InvalidParams = -32602;
    public const int InternalError = -32603;

    public const int Timeout = -32000;
    public const int NotInitialized = -32001;
    public const int AlreadyInitialized = -32002;
    public const int UnsupportedProtocolVersion = -32003;
    public const int InvalidCapability = -32004;
    public const int ResourceNotFound = -32005;
    public const int ToolNotFound = -32006;
    public const int PromptNotFound = -32007;
    public const int InvalidProgress = -32008;

    public const int IncompatibleVersion = -12345;
}

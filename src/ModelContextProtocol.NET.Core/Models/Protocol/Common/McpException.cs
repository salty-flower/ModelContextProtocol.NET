using System;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public abstract class McpException : Exception
{
    public JsonRpcErrorData Error { get; }

    protected McpException(
        string message,
        int errorCode,
        Exception? innerException = null,
        object? customData = null
    )
        : base(message, innerException)
    {
        Error = new JsonRpcErrorData
        {
            Code = errorCode,
            Message = message,
            Data =
                customData == null
                    ? innerException
                    : new { InnerException = innerException, CustomData = customData }
        };
    }
}

using System;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common.Exceptions;

public class McpInvalidParamsException(
    string message,
    Exception? innerException = null,
    object? customData = null
) : McpException(message, McpErrorCodes.InvalidParams, innerException, customData) { }
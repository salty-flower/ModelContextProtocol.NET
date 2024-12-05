using System;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common.Exceptions;

public class McpInternalException(
    string message,
    Exception? innerException = null,
    object? customData = null
) : McpException(message, McpErrorCodes.InternalError, innerException, customData) { }

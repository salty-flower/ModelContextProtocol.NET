using System;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common.Exceptions;

public class McpAlreadyInitializedException(
    string message,
    Exception? innerException = null,
    object? customData = null
) : McpException(message, McpErrorCodes.AlreadyInitialized, innerException, customData) { }

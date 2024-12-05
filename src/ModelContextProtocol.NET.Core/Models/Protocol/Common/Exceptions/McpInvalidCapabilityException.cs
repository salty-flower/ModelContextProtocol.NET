using System;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common.Exceptions;

public class McpInvalidCapabilityException(
    string message,
    Exception? innerException = null,
    object? customData = null
) : McpException(message, McpErrorCodes.InvalidCapability, innerException, customData) { }

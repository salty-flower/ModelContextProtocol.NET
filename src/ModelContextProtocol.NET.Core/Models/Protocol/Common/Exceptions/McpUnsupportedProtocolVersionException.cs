using System;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common.Exceptions;

public class McpUnsupportedProtocolVersionException(
    string message,
    Exception? innerException = null,
    object? customData = null
) : McpException(message, McpErrorCodes.UnsupportedProtocolVersion, innerException, customData) { }

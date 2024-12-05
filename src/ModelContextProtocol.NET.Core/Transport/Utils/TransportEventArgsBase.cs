using System;

namespace ModelContextProtocol.NET.Core.Transport.Utils;

/// <summary>
/// Base class for transport-related event arguments.
/// </summary>
public abstract class TransportEventArgsBase : EventArgs
{
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
}

using System;

namespace ModelContextProtocol.NET.Core.Transport.Utils.TransportEventArgs;

/// <summary>
/// Event arguments for when an error occurs during transport operations.
/// </summary>
public class TransportErrorEventArgs : TransportEventArgsBase
{
    public required Exception Exception { get; init; }
    public string? Operation { get; init; }
}

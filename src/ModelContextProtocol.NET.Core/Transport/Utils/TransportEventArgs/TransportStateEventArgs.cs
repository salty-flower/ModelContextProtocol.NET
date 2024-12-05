namespace ModelContextProtocol.NET.Core.Transport.Utils.TransportEventArgs;

/// <summary>
/// Event arguments for when the transport state changes.
/// </summary>
public class TransportStateEventArgs : TransportEventArgsBase
{
    public required TransportState OldState { get; init; }
    public required TransportState NewState { get; init; }
}

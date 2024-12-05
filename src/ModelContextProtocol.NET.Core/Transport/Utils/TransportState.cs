namespace ModelContextProtocol.NET.Core.Transport.Utils;

/// <summary>
/// Represents the current state of a transport.
/// </summary>
public enum TransportState
{
    /// <summary>
    /// Transport is created but not yet connected.
    /// </summary>
    Initial,

    /// <summary>
    /// Transport is connecting.
    /// </summary>
    Connecting,

    /// <summary>
    /// Transport is connected and ready for communication.
    /// </summary>
    Connected,

    /// <summary>
    /// Transport is disconnecting.
    /// </summary>
    Disconnecting,

    /// <summary>
    /// Transport is disconnected.
    /// </summary>
    Disconnected,

    /// <summary>
    /// Transport encountered an error.
    /// </summary>
    Error,

    /// <summary>
    /// Transport is disposed.
    /// </summary>
    Disposed
}

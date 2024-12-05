using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Transport.Utils;

/// <summary>
/// Event arguments for when a message is received or sent.
/// </summary>
public class MessageEventArgs : TransportEventArgsBase
{
    public required JsonRpcMessage Message { get; init; }
}

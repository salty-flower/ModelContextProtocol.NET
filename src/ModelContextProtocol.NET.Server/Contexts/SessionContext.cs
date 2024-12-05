using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Contexts;

internal class SessionContext : ISessionContext
{
    public required Implementation ClientInfo { get; init; }
    public required ClientCapabilities Capabilities { get; init; }

    public static implicit operator SessionContext(InitializeRequest.Parameters a) =>
        new() { ClientInfo = a.ClientInfo, Capabilities = a.Capabilities };
}

using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Contexts;

internal class ServerContext(Implementation serverInfo) : IServerContext
{
    public Implementation ServerInfo { get; } = serverInfo;
}

using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Contexts;

/// <summary>
/// Provides access to server-wide information.
/// </summary>
public interface IServerContext
{
    /// <summary>
    /// Information about the server implementation.
    /// </summary>
    Implementation ServerInfo { get; }
}

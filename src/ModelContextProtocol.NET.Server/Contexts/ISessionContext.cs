using ModelContextProtocol.NET.Core.Models.Protocol.Common;

namespace ModelContextProtocol.NET.Server.Contexts;

/// <summary>
/// Provides access to session-specific information.
/// </summary>
public interface ISessionContext
{
    /// <summary>
    /// Information about the connected client.
    /// </summary>
    Implementation ClientInfo { get; }

    /// <summary>
    /// Client capabilities received during initialization.
    /// </summary>
    ClientCapabilities Capabilities { get; }
}

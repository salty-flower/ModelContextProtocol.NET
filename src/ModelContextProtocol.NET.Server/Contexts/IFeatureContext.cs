namespace ModelContextProtocol.NET.Server.Contexts;

/// <summary>
/// Base interface for feature-specific contexts.
/// </summary>
public interface IFeatureContext
{
    /// <summary>
    /// Server-wide context.
    /// </summary>
    IServerContext ServerContext { get; }

    /// <summary>
    /// Session-specific context.
    /// </summary>
    ISessionContext SessionContext { get; }
}

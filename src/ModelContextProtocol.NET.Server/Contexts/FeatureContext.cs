namespace ModelContextProtocol.NET.Server.Contexts;

/// <summary>
/// Base implementation of feature contexts.
/// </summary>
internal class FeatureContext : IFeatureContext
{
    public required IServerContext ServerContext { get; init; }
    public required ISessionContext SessionContext { get; init; }
}

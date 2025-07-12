namespace ModelContextProtocol.NET.Server.Session;

internal enum SessionState
{
    Created,
    Starting,
    WaitingForInitialization,
    Running,
    Stopped,
    Failed,
}

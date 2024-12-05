using System.Threading;
using ModelContextProtocol.NET.Core.Transport.Base;

namespace ModelContextProtocol.NET.Server.Features.Progress;

/// <summary>
/// Extension methods for progress tracking.
/// </summary>
internal static class ProgressExtensions
{
    /// <summary>
    /// Creates a progress tracker for an operation.
    /// </summary>
    public static ProgressTracker CreateProgressTracker(
        this IMcpTransportBase transport,
        string progressToken,
        int? total = null,
        CancellationToken cancellationToken = default
    ) => new(transport, progressToken, total, cancellationToken);
}

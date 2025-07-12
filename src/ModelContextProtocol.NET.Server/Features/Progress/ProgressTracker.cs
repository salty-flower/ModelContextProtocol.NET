using System;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Notifications;
using ModelContextProtocol.NET.Core.Transport.Base;

namespace ModelContextProtocol.NET.Server.Features.Progress;

/// <summary>
/// Tracks progress of long-running operations and sends progress notifications.
/// </summary>
internal class ProgressTracker(
    IMcpTransportBase transport,
    string progressToken,
    int? total = null,
    CancellationToken cancellationToken = default
) : IDisposable
{
    private readonly CancellationTokenSource? linkedCts =
        // Link to parent cancellation token if provided
        cancellationToken != default
            ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
            : null;

    private int _current;

    /// <summary>
    /// Gets the cancellation token for this operation.
    /// </summary>
    public CancellationToken CancellationToken => linkedCts?.Token ?? cancellationToken;

    /// <summary>
    /// Reports progress increment.
    /// </summary>
    public async Task ReportIncrementAsync(int increment = 1)
    {
        var newValue = Interlocked.Add(ref _current, increment);
        await ReportProgressAsync(newValue);
    }

    /// <summary>
    /// Reports absolute progress value.
    /// </summary>
    public async Task ReportProgressAsync(int current)
    {
        _current = current;
        await SendProgressNotificationAsync();
    }

    private async Task SendProgressNotificationAsync()
    {
        var notification = new ProgressNotification
        {
            Params = new ProgressNotification.Parameters
            {
                ProgressToken = progressToken,
                Progress = _current,
                Total = total,
            },
        };

        await transport.WriteMessageAsync(notification, cancellationToken);
    }

    public void Dispose() { }
}

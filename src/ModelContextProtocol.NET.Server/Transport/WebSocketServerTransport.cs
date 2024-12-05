using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Transport.Base;
using ModelContextProtocol.NET.Core.Transport.Utils;
using ModelContextProtocol.NET.Core.Transport.Utils.TransportEventArgs;

namespace ModelContextProtocol.NET.Server.Transport;

/// <summary>
/// Transport implementation that uses WebSocket for communication.
/// Handles a single WebSocket connection.
/// </summary>
public sealed class WebSocketServerTransport : WebSocketTransportBase
{
    private readonly TaskCompletionSource connectionCompletion = new();
    private TransportState state = TransportState.Initial;

    public event EventHandler<TransportStateEventArgs>? StateChanged;

    /// <summary>
    /// Initializes a new instance of the WebSocket transport.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="webSocket">The WebSocket instance to use.</param>
    public WebSocketServerTransport(ILogger<WebSocketServerTransport> logger, WebSocket webSocket)
        : base(logger, webSocket)
    {
        if (webSocket.State != WebSocketState.Open)
        {
            throw new ArgumentException("WebSocket must be in Open state", nameof(webSocket));
        }
    }

    public override async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (state != TransportState.Initial)
        {
            throw new InvalidOperationException($"Cannot connect in state {state}");
        }

        try
        {
            UpdateState(TransportState.Connecting);

            // WebSocket is already connected at this point
            // Just verify it's still in good state
            if (WebSocketState != WebSocketState.Open)
            {
                throw new InvalidOperationException(
                    $"WebSocket is in invalid state: {WebSocketState}"
                );
            }

            UpdateState(TransportState.Connected);
            Logger.LogInformation("WebSocket transport connected");

            // Complete when the WebSocket is closed
            _ = MonitorWebSocketStateAsync(cancellationToken);

            await connectionCompletion.Task.WaitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            UpdateState(TransportState.Error);
            Logger.LogError(ex, "Failed to connect WebSocket transport");
            throw;
        }
    }

    private async Task MonitorWebSocketStateAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Wait for the first message to complete
            // This will throw when the connection is closed
            await ReadMessageAsync(cancellationToken);
        }
        catch (WebSocketException ex)
        {
            Logger.LogInformation(ex, "WebSocket connection closed");
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            Logger.LogInformation("WebSocket monitoring cancelled");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error monitoring WebSocket state");
        }
        finally
        {
            UpdateState(TransportState.Disconnected);
            connectionCompletion.TrySetResult();
        }
    }

    private void UpdateState(TransportState newState)
    {
        var oldState = state;
        state = newState;

        try
        {
            StateChanged?.Invoke(
                this,
                new TransportStateEventArgs { OldState = oldState, NewState = newState }
            );
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error in state change handler");
        }
    }

    public override async ValueTask DisposeAsync()
    {
        if (isDisposed)
        {
            return;
        }

        UpdateState(TransportState.Disposed);
        connectionCompletion.TrySetCanceled();

        await base.DisposeAsync();
    }
}

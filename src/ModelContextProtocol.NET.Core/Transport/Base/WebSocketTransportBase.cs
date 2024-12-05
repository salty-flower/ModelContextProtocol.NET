using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Json;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Transport.Base;

/// <summary>
/// Base class for WebSocket-based transports.
/// Provides common WebSocket handling functionality.
/// </summary>
public abstract class WebSocketTransportBase(
    ILogger<WebSocketTransportBase> logger,
    WebSocket webSocket
) : McpTransportBase<WebSocketTransportBase>(logger)
{
    private readonly SemaphoreSlim writeLock = new(1, 1);
    private readonly UTF8Encoding encoding = new(false);

    public override async Task<JsonRpcMessage?> ReadMessageAsync(
        CancellationToken cancellationToken = default
    )
    {
        ThrowIfDisposed();

        try
        {
            var buffer = new byte[4096];
            var messageBuilder = new StringBuilder();

            WebSocketReceiveResult result;
            do
            {
                result = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    cancellationToken
                );

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Closing",
                        cancellationToken
                    );
                    return null;
                }

                messageBuilder.Append(encoding.GetString(buffer, 0, result.Count));
            } while (!result.EndOfMessage);

            var json = messageBuilder.ToString();
            Logger.LogTrace("Received message: {Json}", json);
            return JsonRpcSerializer.ParseMessage(json);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error reading message from WebSocket");
            throw;
        }
    }

    public override async Task WriteMessageAsync(
        JsonRpcMessage message,
        CancellationToken cancellationToken = default
    )
    {
        ThrowIfDisposed();

        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        await writeLock.WaitAsync(cancellationToken);
        try
        {
            var json = JsonRpcSerializer.SerializeMessage(message);
            var buffer = encoding.GetBytes(json);

            await webSocket.SendAsync(
                new ArraySegment<byte>(buffer),
                WebSocketMessageType.Text,
                true,
                cancellationToken
            );

            Logger.LogTrace("Sent message: {Json}", json);
        }
        finally
        {
            writeLock.Release();
        }
    }

    public override async ValueTask DisposeAsync()
    {
        if (isDisposed)
        {
            return;
        }

        SetDisposed();

        try
        {
            if (webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Disposing",
                    CancellationToken.None
                );
            }
            webSocket.Dispose();
            writeLock.Dispose();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error disposing WebSocket transport");
        }
    }

    protected WebSocketState WebSocketState => webSocket.State;
}

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Json;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Transport.Base;

/// <summary>
/// Base class for stream-based transports (e.g. stdio, TCP).
/// Provides common stream handling functionality.
/// </summary>
public abstract class StreamTransportBase : McpTransportBase<StreamTransportBase>
{
    private readonly SemaphoreSlim writeLock = new(1, 1);
    private readonly Stream inputStream;
    private readonly Stream outputStream;
    private readonly StreamReader reader;
    private readonly StreamWriter writer;

    protected StreamTransportBase(
        ILogger<StreamTransportBase> logger,
        Stream inputStream,
        Stream outputStream,
        Encoding encoding
    )
        : base(logger)
    {
        this.inputStream = inputStream;
        this.outputStream = outputStream;

        reader = new StreamReader(this.inputStream, encoding);
        writer = new StreamWriter(this.outputStream, encoding) { AutoFlush = true };
    }

    public override async Task<JsonRpcMessage?> ReadMessageAsync(
        CancellationToken cancellationToken = default
    )
    {
        ThrowIfDisposed();

        try
        {
            var json = await reader.ReadLineAsync(cancellationToken);
            if (json == null)
            {
                return null;
            }

            Logger.LogTrace("Received message: {Json}", json);
            return JsonRpcSerializer.ParseMessage(json);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error reading message from stream");
            throw;
        }
    }

    public override async Task WriteMessageAsync(
        JsonRpcMessage message,
        CancellationToken cancellationToken = default
    )
    {
        ThrowIfDisposed();

        ArgumentNullException.ThrowIfNull(message);

        await writeLock.WaitAsync(cancellationToken);
        try
        {
            var json = JsonRpcSerializer.SerializeMessage(message);
            await writer.WriteLineAsync(json);
            await writer.FlushAsync(cancellationToken);

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
        GC.SuppressFinalize(this);

        try
        {
            await writer.DisposeAsync();
            reader.Dispose();
            writeLock.Dispose();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error disposing stream transport");
        }
    }
}

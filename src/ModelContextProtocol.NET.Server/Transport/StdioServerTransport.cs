using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Transport.Base;

namespace ModelContextProtocol.NET.Server.Transport;

/// <summary>
/// Transport implementation that uses standard input/output streams for communication.
/// </summary>
public sealed class StdioServerTransport : StreamTransportBase
{
    public bool IsConnected { get; private set; } = false;

    /// <summary>
    /// Initializes a new instance of the stdio transport.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="inputStream">Optional input stream (defaults to Console.OpenStandardInput).</param>
    /// <param name="outputStream">Optional output stream (defaults to Console.OpenStandardOutput).</param>
    public StdioServerTransport(
        ILogger<StdioServerTransport> logger,
        Stream? inputStream = null,
        Stream? outputStream = null
    )
        : base(
            logger,
            inputStream ?? Console.OpenStandardInput(),
            outputStream ?? Console.OpenStandardOutput(),
            new UTF8Encoding(false)
        )
    {
        // Configure console to avoid any interference
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = new UTF8Encoding(false);
    }

    public override async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected)
            // sleep forever
            await Task.Delay(Timeout.Infinite, cancellationToken);
        ThrowIfDisposed();
        Logger.LogInformation("Stdio transport connected");
        IsConnected = true;
        await Task.CompletedTask;
    }
}

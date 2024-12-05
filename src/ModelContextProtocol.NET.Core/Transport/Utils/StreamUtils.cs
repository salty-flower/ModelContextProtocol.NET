using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelContextProtocol.NET.Core.Transport.Utils;

/// <summary>
/// Utility methods for stream operations.
/// </summary>
public static class StreamUtils
{
    private const int DefaultBufferSize = 4096;

    /// <summary>
    /// Reads a line from a stream asynchronously using a pooled buffer.
    /// </summary>
    public static async Task<string?> ReadLineAsync(
        this Stream stream,
        Encoding encoding,
        CancellationToken cancellationToken = default
    )
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(DefaultBufferSize);
        try
        {
            using var ms = new MemoryStream();
            int bytesRead;
            bool foundNewLine = false;

            while (
                !foundNewLine && (bytesRead = await stream.ReadAsync(buffer, cancellationToken)) > 0
            )
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    if (buffer[i] == (byte)'\n')
                    {
                        ms.Write(buffer, 0, i);
                        foundNewLine = true;
                        break;
                    }
                }

                if (!foundNewLine)
                {
                    ms.Write(buffer, 0, bytesRead);
                }
            }

            if (ms.Length == 0 && !foundNewLine)
            {
                return null;
            }

            var line = encoding.GetString(ms.ToArray()).TrimEnd('\r');
            return line;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    /// <summary>
    /// Writes a line to a stream asynchronously using a pooled buffer.
    /// </summary>
    public static async Task WriteLineAsync(
        this Stream stream,
        string line,
        Encoding encoding,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(line);

        byte[] lineBytes = encoding.GetBytes(line + "\n");
        await stream.WriteAsync(lineBytes, cancellationToken);
    }

    /// <summary>
    /// Creates a stream that can be read from and written to without closing the underlying stream.
    /// </summary>
    public static Stream CreateNonClosingStream(this Stream stream)
    {
        return new NonClosingStreamWrapper(stream);
    }

    [Janitor.SkipWeaving]
    private sealed class NonClosingStreamWrapper(Stream innerStream) : Stream
    {
        private bool _isDisposed;

        public override bool CanRead => innerStream.CanRead;
        public override bool CanSeek => innerStream.CanSeek;
        public override bool CanWrite => innerStream.CanWrite;
        public override long Length => innerStream.Length;
        public override long Position
        {
            get => innerStream.Position;
            set => innerStream.Position = value;
        }

        public override void Flush() => innerStream.Flush();

        public override int Read(byte[] buffer, int offset, int count) =>
            innerStream.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin) =>
            innerStream.Seek(offset, origin);

        public override void SetLength(long value) => innerStream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) =>
            innerStream.Write(buffer, offset, count);

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                // Don't dispose the inner stream
            }
            base.Dispose(disposing);
        }

        public override ValueTask DisposeAsync()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                // Don't dispose the inner stream
            }
            return base.DisposeAsync();
        }
    }
}

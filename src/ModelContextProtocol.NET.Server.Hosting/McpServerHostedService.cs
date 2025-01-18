using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ModelContextProtocol.NET.Server.Hosting;

internal class McpServerHostedService(IMcpServer server) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        server.Start(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        server.Stop(cancellationToken);
        return Task.CompletedTask;
    }
}

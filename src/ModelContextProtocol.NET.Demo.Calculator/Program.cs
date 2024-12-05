using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Demo.Calculator.Handlers;
using ModelContextProtocol.NET.Server.Builder;
using Serilog;

// Create server info
var serverInfo = new Implementation { Name = "Calculator Demo Server", Version = "1.0.0" };
var seriLogger = new LoggerConfiguration()
    .WriteTo.File("log.txt")
    .MinimumLevel.Verbose()
    .CreateLogger();

// Configure and build server
var server = new McpServerBuilder(serverInfo)
    .AddStdioTransport()
    .ConfigureLogging(logging => logging.AddSerilog(seriLogger).SetMinimumLevel(LogLevel.Trace))
    .ConfigureUserServices(services =>
        // Add logging
        services.AddLogging(builder =>
        {
            builder.AddSerilog(seriLogger);
            builder.SetMinimumLevel(LogLevel.Trace);
        })
    )
    .ConfigureTools(tools => tools.AddHandler<CalculatorToolHandler>())
    .Build();

try
{
    server.Start();
    await Task.Delay(-1); // Wait indefinitely
}
finally
{
    server.Stop();
    await server.DisposeAsync();
}

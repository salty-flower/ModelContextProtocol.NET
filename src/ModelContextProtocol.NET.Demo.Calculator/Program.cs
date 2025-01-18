using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;
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
var builder = new McpServerBuilder(serverInfo).AddStdioTransport();
builder.Services.AddLogging(builder =>
    builder.AddSerilog(seriLogger).SetMinimumLevel(LogLevel.Trace)
);
builder.Tools.AddHandler<CalculatorToolHandler>();
builder.Tools.AddFunction(
    name: "Calculator_function_flavor",
    description: "Still performs basic arithmetic operations, but implemented in functional style",
    parameterTypeInfo: CalculatorParametersJsonContext.Default.CalculatorParameters,
    handler: (CalculatorParameters parameters, CancellationToken ct) =>
        Task.FromResult(
            new CallToolResult
            {
                Content = (TextContent)
                    (
                        (double)(
                            parameters.Operation switch
                            {
                                CalculatorOperation.Add => parameters.A + parameters.B,
                                CalculatorOperation.Subtract => parameters.A - parameters.B,
                                CalculatorOperation.Multiply => parameters.A * parameters.B,
                                CalculatorOperation.Divide when parameters.B != 0
                                    => parameters.A / parameters.B,
                                CalculatorOperation.Divide
                                    => throw new DivideByZeroException("Cannot divide by zero"),
                                _
                                    => throw new ArgumentException(
                                        $"Unknown operation: {parameters.Operation}"
                                    ),
                            }
                        )
                    ).ToString()
            }
        )
);

var server = builder.Build();

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

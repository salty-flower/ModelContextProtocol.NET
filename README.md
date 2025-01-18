# ModelContextProtocol.NET

![NuGet Version](https://img.shields.io/nuget/vpre/ModelContextProtocol.NET.Server)

A C# SDK implementation of the Model Context Protocol (MCP).

## Features

### Ready to Use

- ✅ Standard I/O Communication
- ✅ Tool Integration Framework
- ✅ Native AOT Compatible
- ✅ Calculator Demo Implementation

### Under Development

- 🚧 WebSocket Support
- 🚧 Resource Management
- 🚧 Prompt System

## Demo

See `src/ModelContextProtocol.NET.Demo.Calculator` for a fully functional calculator demo that covers:
- Logging setup
- Tool handler implementation
- Request/response handling
- Error management

## Getting Started

Install [the server package](https://www.nuget.org/packages/ModelContextProtocol.NET.Server):

```bash
dotnet add package ModelContextProtocol.NET.Server --prerelease
```

For hosting integration, add [the server hosting package](https://www.nuget.org/packages/ModelContextProtocol.NET.Server.Hosting):

```bash
dotnet add package ModelContextProtocol.NET.Server.Hosting --prerelease
```

### A. Without Hosting
```csharp
// Create server info
var serverInfo = new Implementation { Name = "Calculator Demo Server", Version = "1.0.0" };

// Configure and build server
var builder = new McpServerBuilder(serverInfo).AddStdioTransport();
builder.Services.AddLogging(<see below>);
builder.Tools.AddHandler<YourToolHandler>();
builder.Tools.AddFunction(
    name: "YourToolName",
    description: "YourToolDescription",
    parameterTypeInfo: YourParameterTypeJsonContext.Default.YourParameterType,
    handler: (YourParameterType parameters, CancellationToken ct) => {
        // Your tool implementation
    }
);
// ...

var server = builder.Build();
server.Start();
await Task.Delay(-1); // Wait indefinitely
```


### B. With Hosting

```csharp
var builder = Host.CreateApplicationBuilder();
builder.Services.AddMcpServer(serverInfo, mcp => {
    mcp.AddStdioTransport();
    // same as without hosting
}, keepDefaultLogging: false); // clear default console logging
// ...

var host = builder.Build();
await host.RunAsync();
```

### Logging Configuration

`McpServerBuilder` uses `Microsoft.Extensions.Logging.ILogger` as the logging interface.
When stdio transport is used, logs can't be sent to the console.
You need to configure a logging provider that writes to other logging destination.

If no logging provider is configured, a null logger will be used, resulting in no logs being written.

The generic host builder will add console-based logger by default, namely `ILoggerFactory` and `ILogger<T>`.
`AddMcpServer` will remove them by default for the same reason as above.
If you still want to keep them, set `keepDefaultLogging` to `true` in `AddMcpServer`.

```csharp
// Using Serilog
.ConfigureLogging(logging => logging.AddSerilog(yourSerilogLogger))

// Using NLog
.ConfigureLogging(logging => logging.AddNLog())
```

### Implementing Tools

Tools are implemented as handlers.
For NativeAOT compatibility, a `JsonTypeInfo` is required for the parameter type.
Then you can either implement a handler class to enjoy dependency injection etc.,
or supply a function directly.

More documentation and implementation guide coming soon.


## Technical Details

### Architecture

The project is structured into multiple components:

- **Core Library**: Contains the fundamental protocol implementation
- **Server Components**: Handles communication and request processing
- **Server Hosting**: Integrates MCP server to .NET generic host
- **Demo**: Provides working examples 
  - a calculator application

## Development Status

This project is under active development. While core features like stdio communication and tool integration are complete and usable, some advanced features are still being implemented.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or create issues for bugs and feature requests.

## License

Apache 2.0

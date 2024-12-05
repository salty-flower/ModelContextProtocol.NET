# ModelContextProtocol.NET

A C# SDK implementation of the Model Context Protocol (MCP), enabling seamless integration between AI models and development environments.

## Overview

ModelContextProtocol.NET is a native .NET implementation of the Model Context Protocol, designed to facilitate communication between AI models and development environments. This SDK provides a robust foundation for building AI-powered tools and applications in the .NET ecosystem.

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

The repository includes a fully functional calculator demo that showcases the basic capabilities of the MCP implementation. This demo serves as a practical example of how to integrate and use the protocol in your applications.

## Technical Details

### Architecture
The project is structured into multiple components:
- **Core Library**: Contains the fundamental protocol implementation
- **Server Components**: Handles communication and request processing
- **Demo Implementation**: Provides a working example with a calculator application

### Key Benefits
- **Native AOT Support**: Fully compatible with .NET Native AOT compilation for optimal performance
- **Modular Design**: Clean separation of concerns allowing for flexible implementation
- **Standard Compliance**: Implements the Model Context Protocol specification

## Getting Started

The easiest way to get started is to look at the calculator demo in `src/ModelContextProtocol.NET.Demo.Calculator`. Here's a quick example of how to set up an MCP server:

```csharp
// Create server info
var serverInfo = new Implementation { Name = "Calculator Demo Server", Version = "1.0.0" };

// Configure and build server
var server = new McpServerBuilder(serverInfo)
    .AddStdioTransport()
    // see below for logging configuration
    .ConfigureLogging(logging => ...)
    .ConfigureTools(tools => tools.AddHandler<CalculatorToolHandler>())
    .Build();

// Start the server
server.Start();
```

### Logging Configuration

`McpServerBuilder` uses `Microsoft.Extensions.Logging.ILogger` as the logging interface.
Since stdio transport is used, logs can't be sent to the console.
You need to configure a logging provider that can write to a file or other logging destination.

If no logging provider is configured, a null logger will be used, resulting in no logs being written.

```csharp
// Using Serilog
.ConfigureLogging(logging => logging.AddSerilog(yourSerilogLogger))

// Using NLog
.ConfigureLogging(logging => logging.AddNLog())
```

### Implementing Tools

Tools are implemented as handlers. Here's a simplified example from the calculator demo:

```csharp
public class CalculatorParameters
{
    public required CalculatorOperation Operation { get; init; }
    public required double A { get; init; }
    public required double B { get; init; }
}

public enum CalculatorOperation { Add, Subtract, Multiply, Divide }
```

Check out the complete calculator demo for a full working example including:
- Tool handler implementation
- Request/response handling
- Error management
- Logging setup

More documentation and implementation guide coming soon.

## Development Status

This project is under active development. While core features like stdio communication and tool integration are complete and usable, some advanced features are still being implemented.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or create issues for bugs and feature requests.

## License

Apache 2.0

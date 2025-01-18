using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;
using ModelContextProtocol.NET.Server.Contexts;
using ModelContextProtocol.NET.Server.Features.Tools;
using ModelContextProtocol.NET.Server.Session;

namespace ModelContextProtocol.NET.Demo.Calculator.Handlers;

/// <summary>
/// Parameters for calculator operations.
/// </summary>
public class CalculatorParameters
{
    public required CalculatorOperation Operation { get; init; }
    public required double A { get; init; }
    public required double B { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter<CalculatorOperation>))]
public enum CalculatorOperation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

[JsonSerializable(typeof(CalculatorParameters))]
public partial class CalculatorParametersJsonContext : JsonSerializerContext { }

/// <summary>
/// Handles calculator operations through a resource interface.
/// </summary>
public class CalculatorToolHandler(
    IServerContext serverContext,
    ISessionFacade sessionFacade,
    ILogger<CalculatorToolHandler> logger
) : ToolHandlerBase<CalculatorParameters>(tool, serverContext, sessionFacade)
{
    private static readonly Tool tool =
        new()
        {
            Name = "Calculator",
            Description = "Performs basic arithmetic operations",
            InputSchema =
                CalculatorParametersJsonContext.Default.CalculatorParameters.GetToolSchema()!
        };

    public override JsonTypeInfo JsonTypeInfo =>
        CalculatorParametersJsonContext.Default.CalculatorParameters;

    protected override Task<CallToolResult> HandleAsync(
        CalculatorParameters parameters,
        CancellationToken cancellationToken = default
    )
    {
        var result = parameters.Operation switch
        {
            CalculatorOperation.Add => parameters.A + parameters.B,
            CalculatorOperation.Subtract => parameters.A - parameters.B,
            CalculatorOperation.Multiply => parameters.A * parameters.B,
            CalculatorOperation.Divide when parameters.B != 0 => parameters.A / parameters.B,
            CalculatorOperation.Divide => throw new DivideByZeroException("Cannot divide by zero"),
            _ => throw new ArgumentException($"Unknown operation: {parameters.Operation}")
        };

        var content = new TextContent { Text = result.ToString() };

        logger.LogInformation("Calculated with final content: {content}", content);

        return Task.FromResult(new CallToolResult { Content = new Annotated[] { content } });
    }
}

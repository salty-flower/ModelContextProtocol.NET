using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;
using ModelContextProtocol.NET.Server.Features.Tools;

namespace ModelContextProtocol.NET.Server.Builder;

public static class ToolRegistryExtensions
{
    public static IToolRegistry AddFunction<TParams, TResult>(
        this IToolRegistry registry,
        string name,
        string description,
        JsonTypeInfo<TParams> parameterTypeInfo,
        JsonTypeInfo<TResult> resultTypeInfo,
        Func<TParams, CancellationToken, Task<TResult>> handler,
        JsonSerializerOptions? jsonSerializerOptions = null
    )
        where TParams : class
        where TResult : class
    {
        async Task<CallToolResult> WrapperHandler(
            TParams parameters,
            CancellationToken cancellationToken
        ) =>
            CallToolResult.FromJsonNode(
                JsonSerializer.SerializeToNode(
                    await handler(parameters, cancellationToken),
                    resultTypeInfo
                )!,
                false,
                jsonSerializerOptions
            );

        return registry.AddHandler(
            new FunctionToolHandler<TParams>(
                new Tool
                {
                    Name = name,
                    Description = description,
                    InputSchema = parameterTypeInfo.GetToolSchema()!,
                    OutputSchema = resultTypeInfo.GetToolSchema()!,
                },
                parameterTypeInfo,
                WrapperHandler
            )
        );
    }

    public static IToolRegistry AddFunction<TParams, TResult>(
        this IToolRegistry registry,
        string name,
        string description,
        JsonTypeInfo<TParams> parameterTypeInfo,
        JsonTypeInfo<TResult> resultTypeInfo,
        Func<TParams, Task<TResult>> handler
    )
        where TParams : class
        where TResult : class =>
        registry.AddFunction(
            name,
            description,
            parameterTypeInfo,
            resultTypeInfo,
            (@params, ct) => handler(@params)
        );

    public static IToolRegistry AddFunction<TParams>(
        this IToolRegistry registry,
        string name,
        string description,
        JsonTypeInfo<TParams> parameterTypeInfo,
        Func<TParams, CancellationToken, Task<CallToolResult>> handler
    )
        where TParams : class =>
        registry.AddHandler(
            new FunctionToolHandler<TParams>(
                new Tool
                {
                    Name = name,
                    Description = description,
                    InputSchema = parameterTypeInfo.GetToolSchema()!,
                },
                parameterTypeInfo,
                handler
            )
        );

    public static IToolRegistry AddFunction<TParams>(
        this IToolRegistry registry,
        string name,
        string description,
        JsonTypeInfo<TParams> parameterTypeInfo,
        Func<TParams, Task<CallToolResult>> handler
    )
        where TParams : class =>
        registry.AddFunction(
            name,
            description,
            parameterTypeInfo,
            (@params, ct) => handler(@params)
        );
}

using System;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Server.Features.Tools;

namespace ModelContextProtocol.NET.Server.Builder;

public static class ToolRegistryExtensions
{
    public static IToolRegistry AddFunction<TParams>(
        this IToolRegistry registry,
        string name,
        string description,
        JsonTypeInfo<TParams> parameterTypeInfo,
        Func<TParams, CancellationToken, Task<CallToolResult>> handler
    )
        where TParams : class
    {
        var tool = new Tool
        {
            Name = name,
            Description = description,
            InputSchema = parameterTypeInfo.GetToolSchema()!,
        };

        return registry.AddHandler(
            new FunctionToolHandler<TParams>(tool, parameterTypeInfo, handler)
        );
    }

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

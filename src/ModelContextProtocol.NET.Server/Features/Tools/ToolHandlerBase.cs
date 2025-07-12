﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Responses;
using ModelContextProtocol.NET.Core.Models.Protocol.Common;
using ModelContextProtocol.NET.Server.Contexts;
using ModelContextProtocol.NET.Server.Session;

namespace ModelContextProtocol.NET.Server.Features.Tools;

/// <summary>
/// Base class for implementing tool handlers with strongly-typed parameters.
/// </summary>
public abstract class ToolHandlerBase<TParams>(
    Tool tool,
#pragma warning disable CS9113 // Parameter is unread.
    IServerContext? serverContext,
    ISessionFacade? sessionContext
#pragma warning restore CS9113 // Parameter is unread.
) : IToolHandler
    where TParams : class
{
    protected IServerContext? ServerContext => serverContext;
    protected ISessionContext? SessionContext => sessionContext?.Context;

    public abstract JsonTypeInfo JsonTypeInfo { get; }

    protected ToolHandlerBase(Tool tool)
        : this(tool, null, null) { }

    public Tool Tool => tool;

    /// <summary>
    /// Handles a tool invocation with strongly-typed parameters.
    /// </summary>
    protected abstract Task<CallToolResult> HandleAsync(
        TParams parameters,
        CancellationToken cancellationToken = default
    );

    async Task<CallToolResult> IToolHandler.HandleAsync(
        object parameters,
        CancellationToken cancellationToken
    )
    {
        var typedParams =
            parameters switch
            {
                TParams p => p,
                Dictionary<string, object> dict => (TParams?)
                    JsonSerializer
                        .SerializeToNode(dict, DictSerializerContext.Default.DictionaryStringObject)
                        .Deserialize(JsonTypeInfo),
                _ => null,
            }
            ?? throw new ArgumentException(
                $"Invalid parameters type. Expected {typeof(TParams).Name}, got {parameters.GetType()}:"
                    + parameters.ToString(),
                nameof(parameters)
            );
        return await HandleAsync(typedParams, cancellationToken);
    }
}

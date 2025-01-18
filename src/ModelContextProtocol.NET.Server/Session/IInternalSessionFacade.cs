using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.NET.Core.Models.Protocol.Client.Requests;
using ModelContextProtocol.NET.Server.Features.Prompts;
using ModelContextProtocol.NET.Server.Features.Resources;
using ModelContextProtocol.NET.Server.Features.Tools;

namespace ModelContextProtocol.NET.Server.Session;

internal interface IInternalSessionFacade : ISessionFacade
{
    void Initialize(InitializeRequest.Parameters parameters);
    IReadOnlyDictionary<string, IToolHandler> ToolHandlers { get; }
    IReadOnlyDictionary<string, IResourceHandler> ResourceHandlers { get; }
    IReadOnlyDictionary<string, IPromptHandler> PromptHandlers { get; }
    IServiceScope SessionScope { get; }
    ISet<string> ActiveSubscriptions { get; }
}

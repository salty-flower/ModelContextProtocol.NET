using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.JsonRpc;
using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Base;

public interface IClientMessage { }

public interface IServerMessage { }

public abstract class RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta
{
    [JsonPropertyName("_meta")]
    public abstract TMeta? Metadata { get; init; }

    public class Meta
    {
        public RpcId ProgressToken { get; init; }
    }
}

public abstract class ServerRequest<TParams, TMeta> : Request<TParams, TMeta>, IServerMessage
    where TParams : RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta { }

public abstract class ClientRequest<TParams, TMeta> : Request<TParams, TMeta>, IClientMessage
    where TParams : RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta { }

[JsonPolymorphic]
public abstract class Request<TParams, TMeta> : JsonRpcRequest<TParams>
    where TParams : RequestParams<TMeta>
    where TMeta : RequestParams<TMeta>.Meta { }

using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Models.JsonRpc;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Base;

public class NotificationParams
{
    [JsonPropertyName("_meta")]
    public Meta? Metadata { get; init; }

    public record Meta { }
}

public abstract class ServerNotification<TParams> : Notification<TParams>, IServerMessage
    where TParams : NotificationParams { }

public abstract class ClientNotification<TParams> : Notification<TParams>, IClientMessage
    where TParams : NotificationParams { }

[JsonPolymorphic]
public abstract class Notification<TParams> : JsonRpcNotification<TParams>
    where TParams : NotificationParams { }

using ModelContextProtocol.NET.Core.Models.JsonRpc;
using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Notifications;

public class CancelledNotification
    : Notification<CancelledNotification.Parameters>,
        IClientMessage,
        IServerMessage
{
    public override string Method => "notifications/cancelled";

    public class Parameters : NotificationParams
    {
        public required RpcId RequestId { get; init; }
        public string? Reason { get; init; }
    }
}

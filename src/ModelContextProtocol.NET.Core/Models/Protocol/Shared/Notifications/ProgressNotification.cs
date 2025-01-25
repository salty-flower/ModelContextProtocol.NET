using ModelContextProtocol.NET.Core.Models.JsonRpc.Types;
using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Notifications;

public class ProgressNotification
    : Notification<ProgressNotification.Parameters>,
        IClientMessage,
        IServerMessage
{
    public override string Method => "notifications/progress";

    public class Parameters : NotificationParams
    {
        public required RpcId ProgressToken { get; init; }
        public required int Progress { get; init; }
        public int? Total { get; init; }
    }
}

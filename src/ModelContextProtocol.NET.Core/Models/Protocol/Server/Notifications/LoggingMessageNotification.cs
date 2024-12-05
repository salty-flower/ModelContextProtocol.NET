using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;

public class LoggingMessageNotification : ServerNotification<LoggingMessageNotification.Parameters>
{
    public override string Method => "notifications/message";

    public class Parameters : NotificationParams
    {
        public required string Level { get; init; }
        public string? Logger { get; init; }
        public required object Data { get; init; }
    }
}

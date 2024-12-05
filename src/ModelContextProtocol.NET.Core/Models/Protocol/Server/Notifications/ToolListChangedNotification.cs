using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;

public class ToolListChangedNotification : ServerNotification<NotificationParams>
{
    public override string Method => "notifications/tools/list_changed";
}

using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;

public class ResourceListChangedNotification : ServerNotification<NotificationParams>
{
    public override string Method => "notifications/resources/list_changed";
}

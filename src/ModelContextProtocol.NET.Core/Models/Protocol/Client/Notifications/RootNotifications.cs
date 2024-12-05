using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Notifications;

public class RootsListChangedNotification : ClientNotification<NotificationParams>
{
    public override string Method => "notifications/roots/list_changed";
}

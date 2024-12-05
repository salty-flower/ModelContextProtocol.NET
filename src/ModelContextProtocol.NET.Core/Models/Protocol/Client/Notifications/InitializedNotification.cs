using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Client.Notifications;

public class InitializedNotification : Notification<NotificationParams>
{
    public override string Method => "notifications/initialized";
}

using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;

public class ResourceUpdatedNotification
    : ServerNotification<ResourceUpdatedNotification.Parameters>
{
    public override string Method => "notifications/resources/updated";

    public class Parameters : NotificationParams
    {
        public required string Uri { get; init; }
    }
}

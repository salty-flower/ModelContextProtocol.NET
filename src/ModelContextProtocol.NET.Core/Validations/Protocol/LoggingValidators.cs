using FluentValidation;
using ModelContextProtocol.NET.Core.Models.Protocol.Server.Notifications;

namespace ModelContextProtocol.NET.Core.Validations.Protocol;

public class LoggingMessageNotificationValidator : AbstractValidator<LoggingMessageNotification>
{
    public LoggingMessageNotificationValidator()
    {
        RuleFor(x => x.Params).NotNull();
        RuleFor(x => x.Params!.Level).NotEmpty();
        RuleFor(x => x.Params!.Data).NotNull();
    }
}

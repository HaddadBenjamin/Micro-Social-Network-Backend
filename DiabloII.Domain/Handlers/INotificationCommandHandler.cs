using DiabloII.Domain.Commands.Notifications;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Handlers
{
    public interface INotificationCommandHandler
    {
        Notification Create(CreateANotificationCommand command);
    }
}
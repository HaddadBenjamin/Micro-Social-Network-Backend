using DiabloII.Domain.Commands.Notifications;
using DiabloII.Domain.Handlers.Bases;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Handlers
{
    public interface INotificationCommandHandler :
        ICreateCommandHandler<CreateANotificationCommand, Notification>
    {
    }
}
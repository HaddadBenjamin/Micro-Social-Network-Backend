using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers
{
    public interface INotificationReader : IGetAllReader<Notification>
    {
    }
}
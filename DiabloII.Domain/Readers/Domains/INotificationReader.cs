using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers.Domains
{
    public interface INotificationReader : IReaderGetAll<Notification>
    {
    }
}
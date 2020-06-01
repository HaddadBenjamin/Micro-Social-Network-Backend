using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Queries.Domains.Suggestions;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers.Domains
{
    public interface INotificationReader :
        IReaderGetAll<Notification>,
        IReaderGet<Notification, GetNotificationQuery>
    {
    }
}
using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Readers
{
    public interface INotificationReader
    {
        IReadOnlyCollection<Notification> GetAll();
    }
}
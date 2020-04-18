using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Services.Notifications
{
    public interface INotifier
    {
        void Notify(Notification notification, IReadOnlyCollection<User> users);
    }
}
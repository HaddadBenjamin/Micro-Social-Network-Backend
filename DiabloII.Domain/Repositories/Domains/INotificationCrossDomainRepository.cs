using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Repositories.Domains
{
    public interface INotificationCrossDomainRepository
    {
        IEnumerable<User> GetUsersConcernedByThisNotification(NotificationType notificationType, IEnumerable<User> users);

        IEnumerable<User> GetUsersConcernedByThisNotifier(NotifierType notifierType, IEnumerable<User> users);
    }
}
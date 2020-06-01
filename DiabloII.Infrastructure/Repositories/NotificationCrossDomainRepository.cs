using System.Collections.Generic;
using System.Linq;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Infrastructure.Repositories
{
    public class NotificationCrossDomainRepository : INotificationCrossDomainRepository
    {
        public IEnumerable<User> GetUsersConcernedByThisNotification(NotificationType notificationType, IEnumerable<User> users) => users
            .Where(user => (user.NotificationSetting.AcceptedNotifications & (int)notificationType) != 0);

        public IEnumerable<User> GetUsersConcernedByThisNotifier(NotifierType notifierType, IEnumerable<User> users) => users
            .Where(user => (user.NotificationSetting.AcceptedNotifiers & (int)notifierType) != 0);
    }
}
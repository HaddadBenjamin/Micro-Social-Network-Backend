using System.Collections.Generic;
using DiabloII.Domain.Mappers.Users;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Services.Notifications;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Services.Notifications
{
    public class InAppNotifier : INotifier
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly INotificationCrossDomainRepository _notificationCrossDomainRepository;

        public InAppNotifier(ApplicationDbContext dbContext, INotificationCrossDomainRepository notificationCrossDomainRepository)
        {
            _dbContext = dbContext;
            _notificationCrossDomainRepository = notificationCrossDomainRepository;
        }

        public void Notify(Notification notification, IReadOnlyCollection<User> users)
        {
            var usersConcernedByThisNotifier = _notificationCrossDomainRepository.GetUsersConcernedByThisNotifier(NotifierType.InApp, users);

            foreach (var user in usersConcernedByThisNotifier)
            {
                var userNotification = UserCrossDomainMapper.ToUserNotification(user, notification);

                user.NotificationSetting.UserNotifications.Add(userNotification);
            }

            _dbContext.SaveChanges();
        }
    }
}
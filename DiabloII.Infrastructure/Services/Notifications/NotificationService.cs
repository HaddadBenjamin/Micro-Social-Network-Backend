using System;
using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Services.Notifications;

namespace DiabloII.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationCrossDomainRepository _notificationCrossDomainRepository;

        private readonly IUserRepository _userRepository;

        private readonly IServiceProvider _serviceProvider;

        public IList<INotifier> Notifiers => new List<INotifier>
        {
            (InAppNotifier)_serviceProvider.GetService(typeof(InAppNotifier)),
            (MailNotifier)_serviceProvider.GetService(typeof(MailNotifier)),
        };

        public NotificationService(INotificationCrossDomainRepository notificationCrossDomainRepository, IUserRepository userRepository, IServiceProvider serviceProvider)
        {
            _notificationCrossDomainRepository = notificationCrossDomainRepository;
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
        }

        public void Notify(Notification notification, IReadOnlyCollection<string> concernedUserIds = null)
        {
            var users = concernedUserIds == null ? _userRepository.GetAllUsers() : _userRepository.GetUsers(concernedUserIds);
            var usersConcernedByThisNotification = _notificationCrossDomainRepository.GetUsersConcernedByThisNotification(notification.Type, users);

            foreach (var notifier in Notifiers)
                notifier.Notify(notification, usersConcernedByThisNotification);
        }
    }
}

using System;
using System.Collections.Generic;
using DiabloII.Domain.Configurations;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Domain.Services.Notifications;

namespace DiabloII.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationCrossDomainRepository _notificationCrossDomainRepository;

        private readonly IUserRepository _userRepository;

        private readonly IServiceProvider _serviceProvider;

        private readonly List<INotifier> _notifiers;

        public IReadOnlyCollection<INotifier> Notifiers => _notifiers;


        public NotificationService(INotificationCrossDomainRepository notificationCrossDomainRepository, IUserRepository userRepository, IServiceProvider serviceProvider, SmtpConfiguration smtpConfiguration)
        {
            _notificationCrossDomainRepository = notificationCrossDomainRepository;
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;

            _notifiers = new List<INotifier>();
            _notifiers.Add((InAppNotifier)_serviceProvider.GetService(typeof(InAppNotifier)));
            if (smtpConfiguration.EnableService)
                _notifiers.Add((MailNotifier)_serviceProvider.GetService(typeof(MailNotifier)));

        }

        public void Notify(Notification notification, IReadOnlyCollection<string> concernedUserIds = null)
        {
            var users = concernedUserIds == null ? _userRepository.GetAll() : _userRepository.GetUsers(concernedUserIds);
            var usersConcernedByThisNotification = _notificationCrossDomainRepository.GetUsersConcernedByThisNotification(notification.Type, users);

            foreach (var notifier in Notifiers)
                notifier.Notify(notification, usersConcernedByThisNotification);
        }
    }
}

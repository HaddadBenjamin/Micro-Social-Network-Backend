using System.Collections.Generic;
using System.Linq;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Domain.Services.Notifications;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace DiabloII.Infrastructure.Services.Notifications
{
    public class MailNotifier : INotifier
    {
        private readonly INotificationCrossDomainRepository _notificationCrossDomainRepository;
        private readonly IFluentEmail _email;

        public MailNotifier(INotificationCrossDomainRepository notificationCrossDomainRepository, IFluentEmail email)
        {
            _notificationCrossDomainRepository = notificationCrossDomainRepository;
            _email = email;
        }

        public void Notify(Notification notification, IEnumerable<User> users)
        {
            var userEmails = _notificationCrossDomainRepository
                .GetUsersConcernedByThisNotifier(NotifierType.Mail, users)
                .Where(user => !string.IsNullOrEmpty(user.Email))
                .Select(user => new Address(user.Email))
                .ToList();

            if (userEmails.Any())
            {
                _email
                    .To(userEmails)
                    .Subject(notification.Title)
                    .Body(notification.Content)
                    .Send();
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Services.Notifications;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace DiabloII.Infrastructure.Services.Notifications
{
    public class MailNotifier : INotifier
    {
        private readonly INotificationCrossDomainRepository _notificationCrossDomainRepository;
        private readonly IFluentEmail _email;

        public MailNotifier(INotificationCrossDomainRepository notificationCrossDomainRepository, IFluentEmail _email)
        {
            _notificationCrossDomainRepository = notificationCrossDomainRepository;
            this._email = _email;
        }

        public void Notify(Notification notification, IEnumerable<User> users)
        {
            var userEmails = _notificationCrossDomainRepository
                .GetUsersConcernedByThisNotifier(NotifierType.Mail, users)
                .Select(user => user.Email)
                .Where(email => !string.IsNullOrEmpty(email))
                .Select(email => new Address(email))
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
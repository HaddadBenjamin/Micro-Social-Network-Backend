using System;
using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Application.Responses.Users
{
    public class UserNotificationSettingDto
    {
        public Guid Id { get; set; }

        public IEnumerable<NotificationType> AcceptedNotifications { get; set; }

        public IEnumerable<NotifierType> AcceptedNotifiers { get; set; }

        public ICollection<UserNotificationDto> UserNotifications { get; set; } = new List<UserNotificationDto>();
    }
}
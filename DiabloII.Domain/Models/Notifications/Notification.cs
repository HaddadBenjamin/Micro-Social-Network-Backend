using System;
using System.Collections.Generic;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Models.Notifications
{
    public class Notification
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    }
}
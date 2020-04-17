using System;
using System.Collections.Generic;

namespace DiabloII.Domain.Models.Users
{
    public class UserNotificationSetting
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int AcceptedNotifications { get; set; }

        public int AcceptedNotifiers { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    }
}
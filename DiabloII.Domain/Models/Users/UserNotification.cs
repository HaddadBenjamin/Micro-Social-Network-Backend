using System;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Models.Users
{
    public class UserNotification
    {
        public Guid Id { get; set; }

        public Guid UserNotificationSettingId { get; set; }

        public UserNotificationSetting UserNotificationSetting { get; set; }

        public Guid NotificationId { get; set; }

        public Notification Notification { get; set; }

        public bool HaveBeenRead { get; set; }
    }
}
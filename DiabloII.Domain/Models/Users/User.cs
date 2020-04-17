using System;

namespace DiabloII.Domain.Models.Users
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public UserNotificationSetting NotificationSetting { get; set; }

        public Guid UserNotificationSettingId { get; set; }
    }
}

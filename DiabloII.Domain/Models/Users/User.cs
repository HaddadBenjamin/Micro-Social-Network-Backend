using System;

namespace DiabloII.Domain.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public UserNotificationSetting NotificationSetting { get; set; }
    }
}

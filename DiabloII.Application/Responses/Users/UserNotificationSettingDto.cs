using System;
using System.Collections.Generic;

namespace DiabloII.Application.Responses.Users
{
    public class UserNotificationSettingDto
    {
        public Guid Id { get; set; }

        public int AcceptedNotifications { get; set; }

        public int AcceptedNotifiers { get; set; }

        public ICollection<UserNotificationDto> UserNotifications { get; set; } = new List<UserNotificationDto>();
    }
}
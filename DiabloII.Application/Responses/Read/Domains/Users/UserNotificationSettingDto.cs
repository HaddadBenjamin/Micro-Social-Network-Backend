using System;
using System.Collections.Generic;

namespace DiabloII.Application.Responses.Read.Domains.Users
{
    public class UserNotificationSettingDto
    {
        public Guid Id { get; set; }

        public IEnumerable<string> AcceptedNotifications { get; set; }

        public IEnumerable<string> AcceptedNotifiers { get; set; }

        public ICollection<UserNotificationDto> UserNotifications { get; set; } = new List<UserNotificationDto>();
    }
}
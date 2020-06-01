using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Application.Requests.Write.Users
{
    public class UpdateAUserDto
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public IEnumerable<NotificationType> AcceptedNotifications { get; set; }

        public IEnumerable<NotifierType> AcceptedNotifiers { get; set; }
    }
}
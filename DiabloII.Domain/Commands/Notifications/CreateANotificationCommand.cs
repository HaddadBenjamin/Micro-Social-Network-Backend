using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Commands.Notifications
{
    public class CreateANotificationCommand
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public IReadOnlyCollection<string> ConcernedUserIds { get; set; }
    }
}

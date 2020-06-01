using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Application.Requests.Write.Notifications
{
    public class CreateANotificationDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }
    }
}

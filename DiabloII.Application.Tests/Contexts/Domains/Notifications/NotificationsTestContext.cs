using DiabloII.Application.Responses;
using DiabloII.Application.Responses.Notifications;

namespace DiabloII.Application.Tests.Contexts.Domains.Notifications
{
    public class NotificationsTestContext : INotificationsTestContext
    {
        public ApiResponses<NotificationDto> Resources { get; set; }

        public NotificationDto CreatedResource { get; set; }
    }
}
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Notifications;

namespace DiabloII.Application.Tests.Contexts.Domains.Notifications
{
    public class NotificationsTestContext : INotificationsTestContext
    {
        public ApiResponses<NotificationDto> Resources { get; set; }

        public NotificationDto CreatedResource { get; set; }
    }
}
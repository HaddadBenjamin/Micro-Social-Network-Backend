using System.Collections.Generic;
using DiabloII.Application.Responses.Notifications;

namespace DiabloII.Application.Tests.Contexts.Domains.Notifications
{
    public class NotificationsTestContext : INotificationsTestContext
    {
        public IReadOnlyCollection<NotificationDto> AllResources { get; set; }
     
        public NotificationDto CreatedResource { get; set; }
    }
}
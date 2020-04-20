using System.Collections.Generic;
using DiabloII.Application.Responses.Notifications;

namespace DiabloII.Application.Tests.Contexts.Notifications
{
    public interface INotificationsTestContext
    {
        public IReadOnlyCollection<NotificationDto> AllNotifications { get; set; }

        public NotificationDto CreatedNotification { get; set; }
    }
}
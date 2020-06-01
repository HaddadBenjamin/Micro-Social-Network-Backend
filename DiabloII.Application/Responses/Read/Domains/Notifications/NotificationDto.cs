using System;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Application.Responses.Read.Domains.Notifications
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }
    }
}

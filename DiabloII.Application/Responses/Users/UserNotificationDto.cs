using System;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Application.Responses.Users
{
    public class UserNotificationDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public bool HaveBeenRead { get; set; }
    }
}
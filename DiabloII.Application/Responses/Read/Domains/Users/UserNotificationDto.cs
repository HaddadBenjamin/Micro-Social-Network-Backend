using System;

namespace DiabloII.Application.Responses.Read.Domains.Users
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
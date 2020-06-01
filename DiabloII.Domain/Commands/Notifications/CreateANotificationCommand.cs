using System;
using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;
using MediatR;

namespace DiabloII.Domain.Commands.Notifications
{
    public class CreateANotificationCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public IReadOnlyCollection<string> ConcernedUserIds { get; set; }
    }
}

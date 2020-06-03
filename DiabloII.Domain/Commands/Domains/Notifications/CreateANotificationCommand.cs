using System;
using System.Collections.Generic;
using DiabloII.Domain.Commands.Bases;
using DiabloII.Domain.Models.Notifications;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Notifications
{
    public class CreateANotificationCommand : IRequest, ICreateCommand
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public IReadOnlyCollection<string> ConcernedUserIds { get; set; }

        public Guid Id { get; set; }
    }
}

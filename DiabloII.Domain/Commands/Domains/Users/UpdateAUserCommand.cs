using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Users
{
    public class UpdateAUserCommand : IRequest, IUpdateCommand<string>
    {
        public string Email { get; set; }

        public int AcceptedNotifications { get; set; }

        public int AcceptedNotifiers { get; set; }

        public string Id { get; set; }
    }
}
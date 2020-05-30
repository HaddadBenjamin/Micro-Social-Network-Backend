using DiabloII.Domain.Models.Users;
using MediatR;

namespace DiabloII.Domain.Commands.Users
{
    public class UpdateAUserCommand : IRequest<User>
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public int AcceptedNotifications { get; set; }

        public int AcceptedNotifiers { get; set; }
    }
}
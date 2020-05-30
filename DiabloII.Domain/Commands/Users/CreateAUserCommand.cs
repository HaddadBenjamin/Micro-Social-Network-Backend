using DiabloII.Domain.Models.Users;
using MediatR;

namespace DiabloII.Domain.Commands.Users
{
    public class CreateAUserCommand : IRequest<User>
    {
        public string UserId { get; set; }

        public string Email { get; set; }
    }
}
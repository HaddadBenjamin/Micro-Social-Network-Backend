using MediatR;

namespace DiabloII.Domain.Commands.Users
{
    public class CreateAUserCommand : IRequest<string>
    {
        public string UserId { get; set; }

        public string Email { get; set; }
    }
}

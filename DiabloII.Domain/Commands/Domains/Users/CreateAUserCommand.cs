using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Users
{
    public class CreateAUserCommand : IRequest, ICreateCommand<string>
    {
        public string Email { get; set; }

        public string Id { get; set; }
    }
}

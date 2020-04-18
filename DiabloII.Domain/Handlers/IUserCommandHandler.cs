using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Handlers
{
    public interface IUserCommandHandler
    {
        User Create(CreateAUserCommand command);

        User Update(UpdateAUserCommand command);
    }
}
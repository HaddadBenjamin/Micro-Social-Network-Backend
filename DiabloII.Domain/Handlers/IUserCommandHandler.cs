using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Handlers.Bases;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Handlers
{
    public interface IUserCommandHandler :
        ICommandHandlerCreate<CreateAUserCommand, User>,
        ICommandHandlerUpdate<UpdateAUserCommand, User>
    {
    }
}
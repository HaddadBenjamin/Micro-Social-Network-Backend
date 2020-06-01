using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Domain.Validations.Users.Create
{
    public class CreateAUserValidationContext
    {
        public CreateAUserCommand Command { get; set; }

        public CommonUserRepositoryValidationContext RepositoryValidationContext { get; }

        public CreateAUserValidationContext(CreateAUserCommand command, IUserRepository repository)
        {
            Command = command;
            RepositoryValidationContext = new CommonUserRepositoryValidationContext(repository)
            {
                Id = Command.UserId,
                Email = Command.Email
            };
        }
    }
}
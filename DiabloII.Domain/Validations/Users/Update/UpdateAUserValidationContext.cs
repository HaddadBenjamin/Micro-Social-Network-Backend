using DiabloII.Domain.Commands.Domains.Users;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Domain.Validations.Users.Update
{
    public class UpdateAUserValidationContext
    {
        public UpdateAUserCommand Command { get; set; }

        public CommonUserRepositoryValidationContext RepositoryValidationContext { get; }

        public UpdateAUserValidationContext(UpdateAUserCommand command, IUserRepository repository)
        {
            Command = command;
            RepositoryValidationContext = new CommonUserRepositoryValidationContext(repository)
            {
                Id = Command.Id,
                Email = Command.Email
            };
        }
    }
}
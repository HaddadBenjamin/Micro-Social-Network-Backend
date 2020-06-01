using DiabloII.Domain.Repositories;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Domain.Validations.Users
{
    public class CommonUserRepositoryValidationContext
    {
        public IUserRepository Repository { get; set; }

        public string Id { get; set; }

        public string Email { get; set; }

        public CommonUserRepositoryValidationContext(IUserRepository repository) => Repository = repository;
    }
}
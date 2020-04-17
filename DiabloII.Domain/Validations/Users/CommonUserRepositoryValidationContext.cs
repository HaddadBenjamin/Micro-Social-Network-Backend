using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Users
{
    public class CommonUserRepositoryValidationContext
    {
        public IUserRepository Repository { get; set; }

        public string Id { get; set; }

        public CommonUserRepositoryValidationContext(IUserRepository repository) => Repository = repository;
    }
}
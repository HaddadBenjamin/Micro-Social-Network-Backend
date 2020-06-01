using System.Collections.Generic;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Readers.Domains;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Infrastructure.Readers
{
    public class UserReader : IUserReader
    {
        private readonly IUserRepository _repository;

        public UserReader(IUserRepository repository) => _repository = repository;

        public IReadOnlyCollection<User> GetAll() => _repository.GetAll();
    }
}
using System.Collections.Generic;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Readers
{
    public interface IUserReader
    {
        IReadOnlyCollection<User> GetAll();
    }
}
using System.Collections.Generic;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories
{
    public interface IUserRepository :
        IGetAllRepository<User>,
        IGetRepository<User, string>
    {
        #region Read
        IEnumerable<User> GetUsers(IReadOnlyCollection<string> userIds);
       
        string GetUserIdByItsEmail(string email);

        bool DoesUserExists(string userId);

        bool DoesEmailIsUnique(string email);
        #endregion
    }
}
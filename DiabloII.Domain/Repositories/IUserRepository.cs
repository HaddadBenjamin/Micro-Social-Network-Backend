using System.Collections.Generic;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Repositories
{
    public interface IUserRepository
    {
        #region Read
        IReadOnlyCollection<User> GetAllUsers();
        
        IEnumerable<User> GetUsers(IReadOnlyCollection<string> userIds);
       
        User GetUser(string user);

        string GetUserIdByItsEmail(string email);

        bool DoesUserExists(string userId);

        bool DoesEmailIsUnique(string email);
        #endregion
    }
}
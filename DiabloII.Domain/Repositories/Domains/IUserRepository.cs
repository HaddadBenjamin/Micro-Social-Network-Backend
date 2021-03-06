﻿using System.Collections.Generic;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories.Domains
{
    public interface IUserRepository :
        IRepositoryGetAll<User>,
        IRepositoryGet<User, string>
    {
        #region Read
        IEnumerable<User> GetUsers(IReadOnlyCollection<string> userIds);

        string GetUserIdByItsEmail(string email);

        User GetUserOrDefaultByItsId(string userId);

        bool DoesUserExists(string userId);

        bool DoesEmailIsUnique(string email);
        #endregion
    }
}
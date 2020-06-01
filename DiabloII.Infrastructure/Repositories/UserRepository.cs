using System.Collections.Generic;
using System.Linq;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DiabloII.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        #region Read
        public IIncludableQueryable<User, Notification> GetQueryableUsers() => _dbContext.Users
            .Include(user => user.NotificationSetting)
            .ThenInclude(UserNotificationSetting => UserNotificationSetting.UserNotifications)
            .ThenInclude(userNotification => userNotification.Notification);

        public IReadOnlyCollection<User> GetAll() => GetQueryableUsers().ToList();

        public IEnumerable<User> GetUsers(IReadOnlyCollection<string> userIds) => GetQueryableUsers().Where(user => userIds.Contains(user.Id));

        public User Get(string userId) => GetQueryableUsers().Single(user => user.Id == userId);

        public string GetUserIdByItsEmail(string email) => GetQueryableUsers().Single(user => user.Email == email).Id;

        public User GetUserOrDefaultByItsId(string id) => GetQueryableUsers().SingleOrDefault(user => user.Id == id);

        public bool DoesUserExists(string userId) => _dbContext.Users.Any(user => user.Id == userId);

        public bool DoesEmailIsUnique(string email) => !_dbContext.Users.Any(user => user.Email == email);
        #endregion
    }
}
using System.Linq;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DiabloII.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public IIncludableQueryable<User, UserNotificationSetting> GetQueryableUsers() => _dbContext.Users
            .Include(user => user.NotificationSetting);

        public bool DoesUserExists(string userId) => _dbContext.Users.Any(user => user.Id == userId);

        public User GetUser(string userId) => GetQueryableUsers().Single(user => user.Id == userId);
    }
}
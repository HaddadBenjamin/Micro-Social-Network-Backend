using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Users
{
    [Binding]
    [Scope(Tag = "users")]
    public class UsersTableCleanerSteps
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersTableCleanerSteps(ApplicationDbContext dbContext) => _dbContext = dbContext;

        [BeforeScenario]
        public void EmptyTheUserTables()
        {
            _dbContext.EmptyTheTable(_dbContext.UserNotifications);
            _dbContext.EmptyTheTable(_dbContext.UserNotificationSettings);
            _dbContext.EmptyTheTable(_dbContext.Users);

            _dbContext.SaveChanges();
        }
    }
}
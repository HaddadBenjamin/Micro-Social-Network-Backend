using DiabloII.Infrastructure.DbContext;
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
        public void EmptyTheSuggestionTables()
        {
            _dbContext.UserNotifications.RemoveRange(_dbContext.UserNotifications);
            _dbContext.UserNotificationSettings.RemoveRange(_dbContext.UserNotificationSettings);
            _dbContext.Users.RemoveRange(_dbContext.Users);

            _dbContext.SaveChanges();
        }
    }
}
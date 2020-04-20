using DiabloII.Infrastructure.DbContext;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions
{
    [Binding]
    [Scope(Tag = "notifications")]
    public class NotificationsTableCleanerSteps
    {
        private readonly ApplicationDbContext _dbContext;

        public NotificationsTableCleanerSteps(ApplicationDbContext dbContext) => _dbContext = dbContext;

        [BeforeScenario]
        public void EmptyTheNotificationTables()
        {
            _dbContext.Notifications.RemoveRange(_dbContext.Notifications);

            _dbContext.SaveChanges();
        }
    }
}
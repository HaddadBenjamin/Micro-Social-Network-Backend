using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Notifications
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
            _dbContext.EmptyTheTable(_dbContext.Notifications);

            _dbContext.SaveChanges();
        }
    }
}
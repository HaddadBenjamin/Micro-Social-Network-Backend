using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Domains.Notifications;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Notifications.GetAll
{
    [Binding]
    [Scope(Tag = "notifications")]
    public class GetAllNotificationSteps
    {
        private readonly INotifications _notifications;
        
        private readonly INotificationsTestContext _notificationsContext;

        public GetAllNotificationSteps(INotifications notifications, INotificationsTestContext notificationsContext)
        {
            _notifications = notifications;
            _notificationsContext = notificationsContext;
        }

        [When(@"I get all the notifications")]
        public async Task WhenIGetAllTheNotifications() =>
            _notificationsContext.AllResources = await _notifications.GetAll();

        [Then(@"all the notifications should be")]
        public void ThenAllTheNotificationsShouldBe(Table table) =>
            table.ShouldAllExistsIn(_notificationsContext.AllResources);
    }
}

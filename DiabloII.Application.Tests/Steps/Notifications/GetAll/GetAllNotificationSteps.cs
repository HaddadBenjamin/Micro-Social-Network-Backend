using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Notifications;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Notifications.GetAll
{
    [Binding]
    [Scope(Tag = "notifications")]
    public class GetAllNotificationSteps
    {
        private readonly INotificationsApi _notificationsApi;
        
        private readonly INotificationsTestContext _notificationsContext;

        public GetAllNotificationSteps(INotificationsApi notificationsApi, INotificationsTestContext notificationsContext)
        {
            _notificationsApi = notificationsApi;
            _notificationsContext = notificationsContext;
        }

        [When(@"I get all the notifications")]
        public async Task WhenIGetAllTheNotifications() =>
            _notificationsContext.AllNotifications = await _notificationsApi.GetAll();

        [Then(@"all the notifications should be")]
        public void ThenAllTheNotificationsShouldBe(Table table) =>
            table.ShouldAllExistsIn(_notificationsContext.AllNotifications);
    }
}

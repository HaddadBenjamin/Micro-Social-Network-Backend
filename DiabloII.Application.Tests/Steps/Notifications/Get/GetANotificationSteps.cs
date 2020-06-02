using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Domains.Notifications;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Notifications.Get
{
    [Binding]
    [Scope(Tag = "notifications")]
    public class GetANotificationSteps
    {
        private readonly INotificationsApi _notificationsApi;

        private readonly INotificationsTestContext _notificationsContext;

        public GetANotificationSteps(INotificationsApi notificationsApi, INotificationsTestContext notificationsContext)
        {
            _notificationsApi = notificationsApi;
            _notificationsContext = notificationsContext;
        }

        [When(@"I get the created notification")]
        public async Task WhenIGetTheCreatedNotification()
        {
            var createdNotificationId = _notificationsContext.CreatedResourceId;

            _notificationsContext.GetResource = await _notificationsApi.Get(createdNotificationId);
        }

        [Then(@"the notification should be")]
        public void ThenTheNotificationShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_notificationsContext.GetResource);
    }
}

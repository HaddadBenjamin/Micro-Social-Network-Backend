using System.Threading.Tasks;
using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Tests.Apis.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Domains.Notifications;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Notifications.Create
{
    [Binding]
    [Scope(Tag = "notifications")]
    public class CreateANotificationSteps
    {
        private readonly INotifications _notifications;
      
        private readonly INotificationsTestContext _notificationContext;

        public CreateANotificationSteps(INotifications notifications, INotificationsTestContext notificationContext)
        {
            _notifications = notifications;
            _notificationContext = notificationContext;
        }

        [When(@"I create the notifications with the following informations")]
        [Given(@"I create the notifications with the following informations")]
        public async Task WhenICreateTheNotificationsWithTheFollowingInformations(Table table)
        {
            var dtos = table.CreateSet<CreateANotificationDto>();

            foreach (var dto in dtos)
                _notificationContext.CreatedResource = await _notifications.Create(dto);
        }

        [Then(@"the created notification should be")]
        public void ThenTheCreatedNotificationShouldBe(Table table) => table.ShouldBeEqualsTo(_notificationContext.AllResources);
    }
}
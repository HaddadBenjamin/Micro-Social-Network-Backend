using DiabloII.Application.Responses.Read.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Bases;

namespace DiabloII.Application.Tests.Contexts.Domains.Notifications
{
    public interface INotificationsTestContext :
        ITestContextAll<NotificationDto>,
        ITestContextCreated<NotificationDto>
    {
    }
}
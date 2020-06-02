using DiabloII.Application.Responses.Read.Notifications;
using DiabloII.Application.Tests.Contexts.Bases;

namespace DiabloII.Application.Tests.Contexts.Domains.Notifications
{
    public interface INotificationsTestContext :
        ITestContextAll<NotificationDto>,
        ITestContextGet<NotificationDto>,
        ITestContextCreated
    {
    }
}
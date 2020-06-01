using DiabloII.Application.Requests.Write.Notifications;
using DiabloII.Application.Responses.Read.Notifications;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Notifications
{
    public interface INotificationsApi :
        IApiGetAll<NotificationDto>,
        IApiCreate<CreateANotificationDto, NotificationDto>
    {
    }
}
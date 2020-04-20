using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Responses.Notifications;

namespace DiabloII.Application.Tests.Apis.Notifications
{

    public interface INotificationsApi
    {
        #region Read
        Task<IReadOnlyCollection<NotificationDto>> GetAll();
        #endregion

        #region Write
        Task<NotificationDto> Create(CreateANotificationDto dto);
        #endregion
    }
}
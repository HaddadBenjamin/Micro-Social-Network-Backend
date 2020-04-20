using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Responses.Notifications;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Notifications
{
    public class NotificationsApi : BaseApi, INotificationsApi
    {
        protected override string BaseUrl { get; } = "notifications";

        public NotificationsApi(IHttpService httpService) : base(httpService) { }

        #region Read
        public async Task<IReadOnlyCollection<NotificationDto>> GetAll() =>
            await _httpService.GetAsync<IReadOnlyCollection<NotificationDto>>(BaseUrl);
        #endregion

        #region Write
        public async Task<NotificationDto> Create(CreateANotificationDto dto) =>
            await _httpService.PostAsync<NotificationDto>(BaseUrl, dto);
        #endregion
    }
}
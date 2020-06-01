using System.Threading.Tasks;
using DiabloII.Application.Requests.Write.Notifications;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Notifications;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Domains.Notifications
{
    public class NotificationsApi : BaseApi, INotificationsApi
    {
        protected override string BaseUrl { get; } = "notifications";

        public NotificationsApi(IHttpService httpService) : base(httpService) { }

        #region Read
        public async Task<ApiResponses<NotificationDto>> GetAll() =>
            await _httpService.GetAsync<ApiResponses<NotificationDto>>(BaseUrl);
        #endregion

        #region Write
        public async Task<NotificationDto> Create(CreateANotificationDto dto) =>
            await _httpService.PostAsync<NotificationDto>(BaseUrl, dto);
        #endregion
    }
}
using System.Threading.Tasks;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Users;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Users
{
    public class UsersApi : AApi, IUsersApi
    {
        protected override string BaseUrl { get; } = "users";
     
        public UsersApi(IHttpService httpService) : base(httpService) { }

        #region Write
        public async Task<UserDto> Create(CreateAUserDto dto) =>
            await _httpService.PostAsync<UserDto>(BaseUrl, dto);
        #endregion
    }
}
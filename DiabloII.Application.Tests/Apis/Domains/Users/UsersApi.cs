using System.Threading.Tasks;
using DiabloII.Application.Requests.Write.Users;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Users;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Domains.Users
{
    public class UsersApi : BaseApi, IUsersApi
    {
        protected override string BaseUrl { get; } = "users";

        public UsersApi(IHttpService httpService) : base(httpService) { }

        #region Read
        public async Task<ApiResponses<UserDto>> GetAll() =>
            await _httpService.GetAsync<ApiResponses<UserDto>>(BaseUrl);

        public async Task<UserDto> Get(string userId) =>
            await _httpService.GetAsync<UserDto>($"{BaseUrl}/{userId}");

        public async Task<UserDto> IdentifyMe() =>
            await _httpService.GetAsync<UserDto>($"{BaseUrl}/identifyme");
        #endregion

        #region Write
        public async Task<string> Create(CreateAUserDto dto) =>
            await _httpService.PostAsync<string>(BaseUrl, dto);

        public async Task<string> Update(UpdateAUserDto dto) =>
            await _httpService.PutAsync<string>($"{BaseUrl}/{dto.UserId}", dto);
        #endregion
    }
}
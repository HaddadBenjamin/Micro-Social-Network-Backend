using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Users;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Domains.Users
{
    public class UsersApi : BaseApi, IUsersApi
    {
        protected override string BaseUrl { get; } = "users";
     
        public UsersApi(IHttpService httpService) : base(httpService) { }

        #region Read
        public async Task<IReadOnlyCollection<UserDto>> GetAll() =>
            await _httpService.GetAsync<IReadOnlyCollection<UserDto>>(BaseUrl);
        #endregion

        #region Write
        public async Task<UserDto> Create(CreateAUserDto dto) =>
            await _httpService.PostAsync<UserDto>(BaseUrl, dto);

        public async Task<UserDto> Update(UpdateAUserDto dto) =>
            await _httpService.PutAsync<UserDto>($"{BaseUrl}/{dto.UserId}", dto);
        #endregion
    }
}
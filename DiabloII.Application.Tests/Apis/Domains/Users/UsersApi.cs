﻿using System.Threading.Tasks;
using DiabloII.Application.Requests.Write.Users;
using DiabloII.Application.Responses;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Domains.Users;
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

        public async Task<UserDto> IdentifyMe() =>
            await _httpService.GetAsync<UserDto>($"{BaseUrl}/identifyme");
        #endregion

        #region Write
        public async Task<UserDto> Create(CreateAUserDto dto) =>
            await _httpService.PostAsync<UserDto>(BaseUrl, dto);

        public async Task<UserDto> Update(UpdateAUserDto dto) =>
            await _httpService.PutAsync<UserDto>($"{BaseUrl}/{dto.UserId}", dto);
        #endregion
    }
}
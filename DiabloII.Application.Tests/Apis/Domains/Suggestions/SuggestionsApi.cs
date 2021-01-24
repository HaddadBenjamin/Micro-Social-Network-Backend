using System;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Write.Suggestions;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Models.Hals.Domains.Suggestions;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Domains.Suggestions
{
    public class SuggestionsApi : BaseApi, ISuggestionsApi
    {
        protected override string BaseUrl { get; } = "suggestions";

        public SuggestionsApi(IHttpService httpService) : base(httpService) { }

        #region Read
        public async Task<ApiResponses<SuggestionDto>> GetAll() =>
            await _httpService.GetAsync<ApiResponses<SuggestionDto>>(BaseUrl);

        public async Task<HalSuggestions> GetAllWithHals() =>
            await _httpService.GetAsync<HalSuggestions>(BaseUrl);

        public async Task<SuggestionDto> Get(Guid suggestionId) =>
            await _httpService.GetAsync<SuggestionDto>($"{BaseUrl}/{suggestionId}");
        #endregion

        #region Write
        public async Task<Guid> Create(CreateASuggestionDto dto) =>
            await _httpService.PostAsync<Guid>(BaseUrl, dto);

        public async Task<Guid> Create(VoteToASuggestionDto dto) =>
            await _httpService.PostAsync<Guid>($"{BaseUrl}/{dto.SuggestionId}/votes", dto);

        public async Task<Guid> Create(CommentASuggestionDto dto) =>
            await _httpService.PostAsync<Guid>($"{BaseUrl}/{dto.SuggestionId}/comments", dto);

        public async Task<Guid> Delete(DeleteASuggestionDto dto) =>
            await _httpService.DeleteAsync<Guid>($"{BaseUrl}/{dto.Id}", dto);

        public async Task<Guid> Delete(DeleteASuggestionCommentDto dto) =>
            await _httpService.DeleteAsync<Guid>($"{BaseUrl}/{dto.SuggestionId}/comments/{dto.Id}", dto);
        #endregion
    }
}
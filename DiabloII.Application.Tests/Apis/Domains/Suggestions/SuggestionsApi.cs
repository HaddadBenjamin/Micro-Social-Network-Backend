using System;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Apis.Bases;
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
        #endregion

        #region Write
        public async Task<SuggestionDto> Create(CreateASuggestionDto dto) =>
            await _httpService.PostAsync<SuggestionDto>(BaseUrl, dto);

        public async Task<SuggestionDto> Create(VoteToASuggestionDto dto) =>
            await _httpService.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/votes", dto);

        public async Task<SuggestionDto> Create(CommentASuggestionDto dto) =>
            await _httpService.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments", dto);

        public async Task<Guid> Delete(DeleteASuggestionDto dto) =>
            await _httpService.DeleteAsync<Guid>($"{BaseUrl}/{dto.Id}", dto);

        public async Task<SuggestionDto> Delete(DeleteASuggestionCommentDto dto) =>
            await _httpService.DeleteAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments/{dto.Id}", dto);
        #endregion
    }
}
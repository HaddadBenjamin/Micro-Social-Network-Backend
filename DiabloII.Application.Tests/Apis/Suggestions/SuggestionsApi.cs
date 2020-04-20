using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Suggestions
{
    public class SuggestionsApi : AApi, ISuggestionsApi
    {
        protected override string BaseUrl { get; } = "suggestions";

        public SuggestionsApi(IHttpService httpService) : base(httpService) { }

        #region Read

        public async Task<IReadOnlyCollection<SuggestionDto>> GetAll() =>
            await _httpService.GetAsync<IReadOnlyCollection<SuggestionDto>>(BaseUrl);

        #endregion

        #region Write

        public async Task<SuggestionDto> Create(CreateASuggestionDto dto) =>
            await _httpService.PostAsync<SuggestionDto>(BaseUrl, dto);

        public async Task<SuggestionDto> Vote(VoteToASuggestionDto dto) =>
            await _httpService.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/votes", dto);

        public async Task<SuggestionDto> Comment(CommentASuggestionDto dto) =>
            await _httpService.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments", dto);

        public async Task Delete(DeleteASuggestionDto dto) =>
            await _httpService.DeleteAsync<Guid>($"{BaseUrl}/{dto.Id}", dto);

        public async Task DeleteComment(DeleteASuggestionCommentDto dto) =>
            await _httpService.DeleteAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments/{dto.Id}", dto);

        #endregion
    }
}
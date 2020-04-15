using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Apis.Suggestions
{
    public class SuggestionsApi : ISuggestionsApi
    {
        private readonly IHttpContext _httpContext;

        private static readonly string BaseUrl = "suggestions";

        public SuggestionsApi(IHttpContext httpContext) => _httpContext = httpContext;

        #region Read
        public async Task<IReadOnlyCollection<SuggestionDto>> GetAll() =>
            await _httpContext.GetAsync<IReadOnlyCollection<SuggestionDto>>(BaseUrl);
        #endregion

        #region Write
        public async Task<SuggestionDto> Create(CreateASuggestionDto dto) =>
            await _httpContext.PostAsync<SuggestionDto>(BaseUrl, dto);

        public async Task<SuggestionDto> Vote(VoteToASuggestionDto dto) =>
            await _httpContext.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/votes", dto);

        public async Task<SuggestionDto> Comment(CommentASuggestionDto dto) =>
            await _httpContext.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments", dto);

        public async Task Delete(DeleteASuggestionDto dto) =>
            await _httpContext.DeleteAsync<Guid>($"{BaseUrl}/{dto.Id}", dto);

        public async Task DeleteComment(DeleteASuggestionCommentDto dto) =>
            await _httpContext.DeleteAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments/{dto.Id}", dto);
        #endregion
    }
}
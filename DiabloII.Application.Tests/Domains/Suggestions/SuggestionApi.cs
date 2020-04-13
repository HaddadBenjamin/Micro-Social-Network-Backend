using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Domains.Suggestions
{
    public class SuggestionApi
    {
        private readonly MyHttpClient _httpClient;

        private static readonly string BaseUrl = "suggestions";

        public SuggestionApi(MyHttpClient httpClient) => _httpClient = httpClient;

        public async Task<IReadOnlyCollection<SuggestionDto>> GetAll() =>
            await _httpClient.GetAsync<IReadOnlyCollection<SuggestionDto>>(BaseUrl);

        public async Task<SuggestionDto> Create(CreateASuggestionDto dto) =>
            await _httpClient.PostAsync<SuggestionDto>(BaseUrl, dto);

        public async Task<SuggestionDto> Vote(VoteToASuggestionDto dto) =>
            await _httpClient.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/votes", dto);

        public async Task<SuggestionDto> Comment(CommentASuggestionDto dto) =>
            await _httpClient.PostAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments", dto);

        public async Task Delete(DeleteASuggestionDto dto) =>
            await _httpClient.DeleteAsync<Guid>($"{BaseUrl}/{dto.Id}", dto);

        public async Task DeleteComment(DeleteASuggestionCommentDto dto) =>
            await _httpClient.DeleteAsync<SuggestionDto>($"{BaseUrl}/{dto.SuggestionId}/comments/{dto.Id}", dto);
    }
}
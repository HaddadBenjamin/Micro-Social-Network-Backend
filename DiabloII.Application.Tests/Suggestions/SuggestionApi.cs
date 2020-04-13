using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Suggestions
{
    public class SuggestionApi
    {
        private readonly MyHttpClient _httpClient;

        public SuggestionApi(MyHttpClient httpClient) => _httpClient = httpClient;

        public async Task<SuggestionDto> Create(CreateASuggestionDto dto) =>
            await _httpClient.PostAsync<SuggestionDto>("suggestions", dto);

        public async Task<IReadOnlyCollection<SuggestionDto>> GetAll() =>
            await _httpClient.GetAsync<IReadOnlyCollection<SuggestionDto>>("suggestions");
    }
}
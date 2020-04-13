using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Suggestions
{
    public class SuggestionApi
    {
        public async Task<SuggestionDto> Create(CreateASuggestionDto dto) =>
            await TestContext.Instance.HttpClient.PostAsync<SuggestionDto>("suggestions", dto);
    }
}
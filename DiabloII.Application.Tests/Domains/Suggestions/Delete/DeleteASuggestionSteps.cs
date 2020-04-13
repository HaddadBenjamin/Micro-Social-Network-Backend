using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Domains.Suggestions.Delete
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class DeleteASuggestionSteps
    {
        private readonly SuggestionApi _suggestionApi;

        public DeleteASuggestionSteps(MyTestContext testContext) => _suggestionApi = testContext.Apis.Suggestions;

        [When(@"I delete the suggestion ""(.*)""")]
        public async Task WhenIDeleteTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = (await _suggestionApi.GetAll())
                .Single(suggestion => suggestion.Content == suggestionContent)
                .Id;
            
            var dto = table.CreateInstance<DeleteASuggestionDto>();
            dto.Id = suggestionId;

            await _suggestionApi.Delete(dto);
        }
    }
}
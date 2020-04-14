using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Delete
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class DeleteASuggestionSteps
    {
        private readonly SuggestionsRepository _suggestionsRepository;
        private readonly SuggestionsApi _suggestionsApi;

        public DeleteASuggestionSteps(TestContext testContext)
        {
            _suggestionsRepository = testContext.RepositoryContext.Suggestions;
            _suggestionsApi = testContext.ApiContext.Suggestions;
        }

        [When(@"I delete the suggestion ""(.*)""")]
        public async Task WhenIDeleteTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = await _suggestionsRepository.GetSuggestionId(suggestionContent);
            var dto = table.CreateInstance<DeleteASuggestionDto>();
            
            dto.Id = suggestionId;

            await _suggestionsApi.Delete(dto);
        }
    }
}
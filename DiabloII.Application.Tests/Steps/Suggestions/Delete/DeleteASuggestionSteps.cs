using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Repositories;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Delete
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class DeleteASuggestionSteps
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
     
        private readonly ISuggestions _suggestions;

        public DeleteASuggestionSteps(ISuggestionsRepository suggestionsRepository, ISuggestions suggestions)
        {
            _suggestionsRepository = suggestionsRepository;
            _suggestions = suggestions;
        }

        [When(@"I delete the suggestion ""(.*)""")]
        public async Task WhenIDeleteTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = await _suggestionsRepository.GetSuggestionId(suggestionContent);
            var dto = table.CreateInstance<DeleteASuggestionDto>();

            dto.Id = suggestionId;

            await _suggestions.Delete(dto);
        }
    }
}
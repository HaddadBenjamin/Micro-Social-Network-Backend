using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.DeleteComment
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class DeleteASuggestionCommentSteps
    {
        private readonly SuggestionsRepository _suggestionsRepository;
        private readonly SuggestionsApi _suggestionsApi;

        public DeleteASuggestionCommentSteps(TestContext testContext)
        {
            _suggestionsRepository = testContext.Repositories.Suggestions;
            _suggestionsApi = testContext.ApiContext.Suggestions;
        }

        [When(@"I delete the suggestion comment ""(.*)"" from the suggestion ""(.*)""")]
        public async Task WhenIDeleteTheSuggestionCommentFromTheSuggestion(string suggestionCommentContent, string suggestionContent, Table table)
        {
            var suggestionDto = await _suggestionsRepository.GetSuggestion(suggestionContent);
            var suggestionCommentId = _suggestionsRepository.GetSuggestionCommentId(suggestionDto, suggestionCommentContent);

            var dto = table.CreateInstance<DeleteASuggestionCommentDto>();
            dto.Id = suggestionCommentId;
            dto.SuggestionId = suggestionDto.Id;

            await _suggestionsApi.DeleteComment(dto);
        }
    }
}
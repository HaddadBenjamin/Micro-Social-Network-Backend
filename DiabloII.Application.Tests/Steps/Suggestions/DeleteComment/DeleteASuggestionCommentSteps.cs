using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Repositories.Suggestions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.DeleteComment
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class DeleteASuggestionCommentSteps
    {
        private readonly ISuggestionsRepository _suggestionsRepository;

        private readonly ISuggestionsApi _suggestionsApi;

        public DeleteASuggestionCommentSteps(ISuggestionsRepository suggestionsRepository, ISuggestionsApi suggestionsApi)
        {
            _suggestionsRepository = suggestionsRepository;
            _suggestionsApi = suggestionsApi;
        }

        [When(@"I delete the suggestion comment ""(.*)"" from the suggestion ""(.*)""")]
        public async Task WhenIDeleteTheSuggestionCommentFromTheSuggestion(string suggestionCommentContent, string suggestionContent, Table table)
        {
            var suggestionDto = await _suggestionsRepository.GetSuggestion(suggestionContent);
            var suggestionCommentId = _suggestionsRepository.GetSuggestionCommentId(suggestionDto, suggestionCommentContent);
            var dto = table.CreateInstance<DeleteASuggestionCommentDto>();

            dto.Id = suggestionCommentId;
            dto.SuggestionId = suggestionDto.Id;

            await _suggestionsApi.Delete(dto);
        }
    }
}
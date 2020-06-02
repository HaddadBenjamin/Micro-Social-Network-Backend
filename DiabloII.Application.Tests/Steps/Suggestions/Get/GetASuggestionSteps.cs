

using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Repositories.Suggestions;
using DiabloII.Domain.Repositories.Domains;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions.Get
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class GetASuggestionSteps
    {
        private readonly ISuggestionsApi _suggestionsApi;

        private readonly ISuggestionsTestContext _suggestionsContext;

        private readonly ISuggestionsRepository _repository;

        public GetASuggestionSteps(ISuggestionsApi suggestionsApi, ISuggestionsTestContext suggestionsContext, ISuggestionsRepository repository)
        {
            _suggestionsApi = suggestionsApi;
            _suggestionsContext = suggestionsContext;
            _repository = repository;
        }

        [When(@"I get the last created suggestion")]
        public async Task WhenIGetTheLastCreatedSuggestion()
        {
            var suggestionId = _suggestionsContext.CreatedResourceId;

            _suggestionsContext.GetResource = await _suggestionsApi.Get(suggestionId);
        }

        [Then(@"the suggestion should be")]
        public void ThenTheSuggestionShouldBe(Table table) => table.ShouldBeEqualsTo(_suggestionsContext.GetResource);

        [Given(@"I get the vote created by ""(.*)"" from the suggestion ""(.*)""")]
        public async Task GivenIGetTheVoteCreatedByFromTheSuggestion(string createdBy, string suggestionContent)
        {
            var suggestion = await _repository.GetByItsContent(suggestionContent);
            var vote = _repository.GetVoteCreatedBy(suggestion, createdBy);

            _suggestionsContext.Vote = vote;
        }

        [Given(@"I get the comment ""(.*)"" from the suggestion ""(.*)""")]
        public async Task GivenIGetTheCommentFromTheSuggestion(string commentContent, string suggestionContent)
        {
            var suggestion = await _repository.GetByItsContent(suggestionContent);
            var comment = _repository.GetCommentByItsContent(suggestion, commentContent);

            _suggestionsContext.Comment = comment;
        }
    }
}
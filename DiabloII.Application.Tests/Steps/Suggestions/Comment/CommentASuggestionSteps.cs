using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Apis;
using DiabloII.Application.Tests.Contexts;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Mappers;
using DiabloII.Application.Tests.Repositories;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Comment
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class CommentASuggestionSteps
    {
        private readonly SuggestionsApi _suggestionsApi;
        private readonly SuggestionsRepository _suggestionsRepository;
        private readonly SuggestionsTestContext _suggestionsContext;

        public CommentASuggestionSteps(TestContext testContext, SuggestionsTestContext suggestionsContext)
        {
            _suggestionsApi = testContext.ApiContext.Suggestions;
            _suggestionsRepository = testContext.RepositoryContext.Suggestions;
            _suggestionsContext = suggestionsContext;
        }

        [Given(@"I comment the suggestion ""(.*)""")]
        [When(@"I comment the suggestion ""(.*)""")]
        public async Task WhenICommentTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = await _suggestionsRepository.GetSuggestionId(suggestionContent);
            var dto = table.CreateInstance<CommentASuggestionDto>();

            dto.SuggestionId = suggestionId;

            _suggestionsContext.VotedSuggestion = await _suggestionsApi.Comment(dto);
        }

        [Then(@"the commented suggestion should be")]
        public void ThenTheCommentedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionsContext.CommentedSuggestion, SuggestionsTableMapper.ToSuggestionDto);
    }
}
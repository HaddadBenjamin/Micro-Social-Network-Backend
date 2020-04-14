using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Comment
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class CommentASuggestionSteps
    {
        private readonly SuggestionsApi _suggestionsApi;
        private readonly SuggestionTestContext _suggestionContext;

        public CommentASuggestionSteps(TestContext testContext, SuggestionTestContext suggestionContext)
        {
            _suggestionsApi = testContext.ApiContext.Suggestions;
            _suggestionContext = suggestionContext;
        }

        [Given(@"I comment the suggestion ""(.*)""")]
        [When(@"I comment the suggestion ""(.*)""")]
        public async Task WhenICommentTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = (await _suggestionsApi.GetAll())
                .Single(suggestion => suggestion.Content == suggestionContent)
                .Id;
            var dto = table.CreateInstance<CommentASuggestionDto>();

            dto.SuggestionId = suggestionId;

            _suggestionContext.VotedSuggestion = await _suggestionsApi.Comment(dto);
        }

        [Then(@"the commented suggestion should be")]
        public void ThenTheCommentedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionContext.CommentedSuggestion, SuggestionTableMapper.ToSuggestionDto);
    }
}
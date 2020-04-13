using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Suggestions.Comment
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class CommentASuggestionSteps
    {
        private readonly SuggestionApi _suggestionApi = MyTestContext.Instance.Apis.Suggestions;
        private readonly SuggestionTestContext _suggestionContext = MyTestContext.Instance.Contexts.Suggestions;

        [When(@"I comment the suggestion ""(.*)""")]
        public async Task WhenICommentTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = (await _suggestionApi.GetAll())
                .Single(suggestion => suggestion.Content == suggestionContent)
                .Id;
            var dto = table.CreateInstance<CommentASuggestionDto>();

            dto.SuggestionId = suggestionId;

            _suggestionContext.VotedSuggestion = await _suggestionApi.Comment(dto);
        }

        [Then(@"the commented suggestion should be")]
        public void ThenTheCommentedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionContext.CommentedSuggestion);
    }
}
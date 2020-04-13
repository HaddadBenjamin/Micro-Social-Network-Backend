using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Suggestions.Vote
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class VoteToASuggestionSteps
    {
        private readonly SuggestionApi _suggestionApi;
        private readonly SuggestionTestContext _suggestionContext;

        public VoteToASuggestionSteps(MyTestContext testContext, SuggestionTestContext suggestionContext)
        {
            _suggestionApi = testContext.Apis.Suggestions;
            _suggestionContext = suggestionContext;
        }

        [When(@"I vote to the suggestion ""(.*)""")]
        public async Task WhenIVoteToTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = (await _suggestionApi.GetAll())
                .Single(suggestion => suggestion.Content == suggestionContent)
                .Id; 
            var dto = table.CreateInstance<VoteToASuggestionDto>();

            dto.SuggestionId = suggestionId;

            _suggestionContext.VotedSuggestion = await _suggestionApi.Vote(dto);
        }

        [Then(@"the voted suggestion should be")]
        public void ThenTheVotedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionContext.VotedSuggestion, SuggestionTableMapper.ToSuggestionDto);
    }
}

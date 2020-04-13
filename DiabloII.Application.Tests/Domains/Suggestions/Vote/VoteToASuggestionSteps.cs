using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Domains.Suggestions.Vote
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class VoteToASuggestionSteps
    {
        private readonly SuggestionsApi _suggestionsApi;
        private readonly SuggestionTestContext _suggestionContext;

        public VoteToASuggestionSteps(MyTestContext testContext, SuggestionTestContext suggestionContext)
        {
            _suggestionsApi = testContext.Apis.Suggestions;
            _suggestionContext = suggestionContext;
        }

        [When(@"I vote to the suggestion ""(.*)""")]
        public async Task WhenIVoteToTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = (await _suggestionsApi.GetAll())
                .Single(suggestion => suggestion.Content == suggestionContent)
                .Id; 
            var dto = table.CreateInstance<VoteToASuggestionDto>();

            dto.SuggestionId = suggestionId;

            _suggestionContext.VotedSuggestion = await _suggestionsApi.Vote(dto);
        }

        [Then(@"the voted suggestion should be")]
        public void ThenTheVotedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionContext.VotedSuggestion, SuggestionTableMapper.ToSuggestionDto);
    }
}

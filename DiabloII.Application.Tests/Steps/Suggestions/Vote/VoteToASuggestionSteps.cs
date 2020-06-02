using System.Threading.Tasks;
using DiabloII.Application.Requests.Write.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Mappers;
using DiabloII.Application.Tests.Repositories.Suggestions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Vote
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class VoteToASuggestionSteps
    {
        private readonly ISuggestionsApi _suggestionsApi;

        private readonly ISuggestionsRepository _suggestionsRepository;

        private readonly ISuggestionsTestContext _suggestionsContext;

        public VoteToASuggestionSteps(ISuggestionsApi suggestionsApi, ISuggestionsRepository suggestionsRepository, ISuggestionsTestContext suggestionsContext)
        {
            _suggestionsApi = suggestionsApi;
            _suggestionsRepository = suggestionsRepository;
            _suggestionsContext = suggestionsContext;
        }

        [Given(@"I vote to the suggestion ""(.*)""")]
        [When(@"I vote to the suggestion ""(.*)""")]
        public async Task WhenIVoteToTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = await _suggestionsRepository.GetIdByItsContent(suggestionContent);
            var dto = table.CreateInstance<VoteToASuggestionDto>();

            dto.SuggestionId = suggestionId;

            await _suggestionsApi.Create(dto);
        }

        [Then(@"the voted suggestion should be")]
        public void ThenTheVotedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionsContext.Vote, SuggestionsTableMapper.ToSuggestionVoteDto);
    }
}

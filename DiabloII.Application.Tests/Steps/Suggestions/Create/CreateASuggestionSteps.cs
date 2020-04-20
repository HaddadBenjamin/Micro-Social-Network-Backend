using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Apis.Suggestions;
using DiabloII.Application.Tests.Contexts.Suggestions;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Create
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class CreateASuggestionSteps
    {
        private readonly ISuggestionsApi _suggestionsApi;
       
        private readonly ISuggestionsTestContext _suggestionsContext;

        public CreateASuggestionSteps(ISuggestionsApi suggestionsApi, ISuggestionsTestContext suggestionsContext)
        {
            _suggestionsApi = suggestionsApi;
            _suggestionsContext = suggestionsContext;
        }

        [Given(@"I create the suggestions with the following informations")]
        [When(@"I create the suggestions with the following informations")]
        public async Task WhenICreateTheSuggestionsWithTheFollowingInformations(Table table)
        {
            var dtos = table.CreateSet<CreateASuggestionDto>();

            foreach (var dto in dtos)
                _suggestionsContext.CreatedSuggestion = await _suggestionsApi.Create(dto);
        }

        [Then(@"the created suggestion should be")]
        public void ThenTheCreatedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionsContext.CreatedSuggestion);
    }
}
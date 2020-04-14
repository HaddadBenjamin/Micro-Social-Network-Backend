using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Create
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class CreateASuggestionSteps
    {
        private readonly SuggestionsApi _suggestionsApi;
        private readonly SuggestionTestContext _suggestionContext;

        public CreateASuggestionSteps(TestContext testContext, SuggestionTestContext suggestionContext)
        {
            _suggestionsApi = testContext.ApiContext.Suggestions;
            _suggestionContext = suggestionContext;
        }

        [Given(@"I create the suggestions with the following informations")]
        [When(@"I create the suggestions with the following informations")]
        public async Task WhenICreateTheSuggestionsWithTheFollowingInformations(Table table)
        {
            var dtos = table.CreateSet<CreateASuggestionDto>();

            foreach (var dto in dtos)
                _suggestionContext.CreatedSuggestion = await _suggestionsApi.Create(dto);
        }

        [Then(@"the created suggestion should be")]
        public void ThenTheCreatedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionContext.CreatedSuggestion);
    }
}
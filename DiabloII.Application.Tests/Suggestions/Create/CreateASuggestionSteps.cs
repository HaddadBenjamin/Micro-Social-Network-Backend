using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Suggestions.Create
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class CreateASuggestionSteps
    {
        private readonly SuggestionApi _suggestionApi = new SuggestionApi();
        private readonly SuggestionTestContext _suggestionContext = new SuggestionTestContext();

        [When(@"I create a suggestion with the following informations")]
        public async Task WhenICreateASuggestionWithTheFollowingInformations(Table table)
        {
            var dto = table.CreateInstance<CreateASuggestionDto>();

            _suggestionContext.CreatedSuggestion = await _suggestionApi.Create(dto);
        }

        [Then(@"the created suggestion should be")]
        public void ThenTheCreatedSuggestionShouldBe(Table table)
        {
            var expectedCreatedSuggestion = table.CreateInstance<SuggestionDto>();

            _suggestionContext.CreatedSuggestion.ShouldBe(expectedCreatedSuggestion);
        }
    }
}
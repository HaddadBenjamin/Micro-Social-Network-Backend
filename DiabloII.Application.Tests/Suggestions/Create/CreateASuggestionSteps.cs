using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Suggestions.Create
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class CreateASuggestionSteps
    {
        private readonly SuggestionApi _suggestionApi = MyTestContext.Instance.Apis.Suggestions;
        private readonly SuggestionTestContext _suggestionContext = MyTestContext.Instance.Contexts.Suggestions;

        [Given(@"I create a suggestion with the following informations")]
        [When(@"I create a suggestion with the following informations")]
        public async Task WhenICreateASuggestionWithTheFollowingInformations(Table table)
        {
            var dtos = table.CreateSet<CreateASuggestionDto>();

            foreach(var dto in dtos)
                _suggestionContext.CreatedSuggestion = await _suggestionApi.Create(dto);
        }

        [Then(@"the created suggestion should be")]
        public void ThenTheCreatedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionContext.CreatedSuggestion);
    }
}
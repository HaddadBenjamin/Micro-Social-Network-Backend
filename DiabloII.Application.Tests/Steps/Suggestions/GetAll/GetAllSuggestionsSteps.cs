using System.Threading.Tasks;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions.GetAll
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class GetAllSuggestionsSteps
    {
        private readonly SuggestionsApi _suggestionsApi;
        private readonly SuggestionTestContext _suggestionContext;

        public GetAllSuggestionsSteps(TestContext testContext, SuggestionTestContext suggestionContext)
        {
            _suggestionsApi = testContext.ApiContext.Suggestions;
            _suggestionContext = suggestionContext;
        }

        [When(@"I get all the suggestions")]
        public async Task WhenIGetAllTheSuggestions() =>
            _suggestionContext.AllSuggestions = await _suggestionsApi.GetAll();
        
        [Then(@"all the suggestions should be")]
        public void WhenAllTheSuggestionsShouldBe(Table table) =>
            table.ShouldAllExistsIn(_suggestionContext.AllSuggestions);
    }
}

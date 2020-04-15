using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis;
using DiabloII.Application.Tests.Contexts;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions.GetAll
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class GetAllSuggestionsSteps
    {
        private readonly SuggestionsApi _suggestionsApi;
        private readonly SuggestionsTestContext _suggestionsContext;

        public GetAllSuggestionsSteps(TestContext testContext, SuggestionsTestContext suggestionsContext)
        {
            _suggestionsApi = testContext.ApiContext.Suggestions;
            _suggestionsContext = suggestionsContext;
        }

        [When(@"I get all the suggestions")]
        public async Task WhenIGetAllTheSuggestions() => _suggestionsContext.AllSuggestions = await _suggestionsApi.GetAll();
        
        [Then(@"all the suggestions should be")]
        public void WhenAllTheSuggestionsShouldBe(Table table) => table.ShouldAllExistsIn(_suggestionsContext.AllSuggestions);
    }
}

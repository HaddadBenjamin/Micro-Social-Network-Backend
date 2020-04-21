using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions.GetAll
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class GetAllSuggestionsSteps
    {
        private readonly ISuggestionsApi _suggestionsApi;
        
        private readonly ISuggestionsTestContext _suggestionsContext;

        public GetAllSuggestionsSteps(ISuggestionsApi suggestionsApi, ISuggestionsTestContext suggestionsContext)
        {
            _suggestionsApi = suggestionsApi;
            _suggestionsContext = suggestionsContext;
        }

        [When(@"I get all the suggestions")]
        public async Task WhenIGetAllTheSuggestions() => _suggestionsContext.AllResources = await _suggestionsApi.GetAll();
        
        [Then(@"all the suggestions should be")]
        public void WhenAllTheSuggestionsShouldBe(Table table) => table.ShouldAllExistsIn(_suggestionsContext.AllResources);
    }
}

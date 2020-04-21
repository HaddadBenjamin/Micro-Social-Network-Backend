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
        private readonly ISuggestions _suggestions;
        
        private readonly ISuggestionsTestContext _suggestionsContext;

        public GetAllSuggestionsSteps(ISuggestions suggestions, ISuggestionsTestContext suggestionsContext)
        {
            _suggestions = suggestions;
            _suggestionsContext = suggestionsContext;
        }

        [When(@"I get all the suggestions")]
        public async Task WhenIGetAllTheSuggestions() => _suggestionsContext.AllResources = await _suggestions.GetAll();
        
        [Then(@"all the suggestions should be")]
        public void WhenAllTheSuggestionsShouldBe(Table table) => table.ShouldAllExistsIn(_suggestionsContext.AllResources);
    }
}

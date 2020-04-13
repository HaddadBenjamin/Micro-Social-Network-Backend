using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Suggestions.GetAll
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class GetAllSuggestionsSteps
    {
        private readonly SuggestionApi _suggestionApi = MyTestContext.Instance.Apis.Suggestions;
        private readonly SuggestionTestContext _suggestionContext = MyTestContext.Instance.Contexts.Suggestions;

        [When(@"I get all the suggestions")]
        public async Task WhenIGetAllTheSuggestions() =>
            _suggestionContext.AllSuggestions = await _suggestionApi.GetAll();
        
        [When(@"all the suggestions should be")]
        public void WhenAllTheSuggestionsShouldBe(Table table) =>
            table.ShouldAllExistsIn(_suggestionContext.AllSuggestions);
    }
}

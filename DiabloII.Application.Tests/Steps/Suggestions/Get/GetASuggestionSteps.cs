using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions.Get
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class GetASuggestionSteps
    {
        private readonly ISuggestionsApi _suggestionsApi;

        private readonly ISuggestionsTestContext _suggestionsContext;

        public GetASuggestionSteps(ISuggestionsApi suggestionsApi, ISuggestionsTestContext suggestionsContext)
        {
            _suggestionsApi = suggestionsApi;
            _suggestionsContext = suggestionsContext;
        }

        [When(@"I get the last created suggestion")]
        public async Task WhenIGetTheLastCreatedSuggestion()
        {
            var suggestionId = _suggestionsContext.CreatedResource.Id;

            _suggestionsContext.GetResource = await _suggestionsApi.Get(suggestionId);
        }

        [Then(@"the suggestion should be")]
        public void ThenTheSuggestionShouldBe(Table table) => table.ShouldBeEqualsTo(_suggestionsContext.GetResource);
    }
}
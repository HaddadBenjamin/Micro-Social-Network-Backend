using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Create
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class CreateASuggestionSteps
    {
        private readonly ISuggestions _suggestions;
       
        private readonly ISuggestionsTestContext _suggestionsContext;

        public CreateASuggestionSteps(ISuggestions suggestions, ISuggestionsTestContext suggestionsContext)
        {
            _suggestions = suggestions;
            _suggestionsContext = suggestionsContext;
        }

        [Given(@"I create the suggestions with the following informations")]
        [When(@"I create the suggestions with the following informations")]
        public async Task WhenICreateTheSuggestionsWithTheFollowingInformations(Table table)
        {
            var dtos = table.CreateSet<CreateASuggestionDto>();

            foreach (var dto in dtos)
                _suggestionsContext.CreatedResource = await _suggestions.Create(dto);
        }

        [Then(@"the created suggestion should be")]
        public void ThenTheCreatedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionsContext.CreatedResource);
    }
}
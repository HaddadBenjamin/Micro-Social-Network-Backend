using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class GetAllHalLinksSuggestionsSteps
    {
        private readonly ISuggestionsApi _suggestionsApi;
      
        private readonly IHalSuggestionsTestContext _halSuggestionsContext;

        public GetAllHalLinksSuggestionsSteps(ISuggestionsApi suggestionsApi, IHalSuggestionsTestContext halSuggestionsContext)
        {
            _suggestionsApi = suggestionsApi;
            _halSuggestionsContext = halSuggestionsContext;
        }

        [When(@"I get all suggestions with hal links")]
        public async Task WhenIGetAllSuggestionsWithHalLinks() =>
            _halSuggestionsContext.HalResources = await _suggestionsApi.GetAllWithHals();

        [Then(@"I should retrieve ""(.*)"" as suggestions hal links")]
        public void ThenIShouldRetrieveAsSuggestionsHalLinks(string halLinks)
        {
            var halResources = _halSuggestionsContext.HalResources;
            var actualSuggestionHalLinks = halResources._Links.Keys;
            var actualVoteHalLinks = halResources.Elements.SelectMany(e => e.Votes).SelectMany(vote => vote._Links.Keys);
            var actualCommentHalLinks = halResources.Elements.SelectMany(e => e.Comments).SelectMany(vote => vote._Links.Keys);

            var actualHalLinks = actualSuggestionHalLinks.Union(actualVoteHalLinks).Union(actualCommentHalLinks);
            var expectedHalLinks = halLinks.Split(';').Where(h => !string.IsNullOrEmpty(h));

            actualHalLinks.ShouldBe(expectedHalLinks, true);
        }
    }
}

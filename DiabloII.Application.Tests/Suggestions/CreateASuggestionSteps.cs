using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Suggestions
{
    [Binding]
    public class CreateASuggestionSteps
    {
        [Given(@"I am a client")]
        public async Task GivenIAmAClient()
        {
            var xxxx4 = await TestContext.Instance.Client.GetAsync("/api/v1/suggestions");
        }

        [When(@"I create a suggestion with the following informations")]
        public async Task WhenICreateASuggestionWithTheFollowingInformations(Table table)
        {
            var createASuggestionDto = table.CreateInstance<CreateASuggestionDto>();
            await TestContext.Instance.Client.PostAsync("/api/v1/suggestions");
        }

        [Then(@"the http status should be (.*)")]
        public void ThenTheHttpStatusShouldBe(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the suggestion created should be")]
        public void ThenTheSuggestionCreatedShouldBe()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
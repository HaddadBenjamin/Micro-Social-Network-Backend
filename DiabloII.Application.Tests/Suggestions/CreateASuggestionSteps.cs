using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Suggestions
{
    [Binding]
    public class CreateASuggestionSteps
    {

        [Given(@"I am a client")]
        public async Task GivenIAmAClient()
        {
            var testContenxt = new TestContext();
            var xxxx4 = await testContenxt.Client.GetAsync("/api/v1/suggestions");

        }
    }
}
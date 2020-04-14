using System.Threading.Tasks;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Items.GetAll
{
    [Binding]
    [Scope(Tag = "items")]
    public class GetAllItemsSteps
    {
        private readonly ItemsApi _itemsApi;

        public GetAllItemsSteps(TestContext testContext) => _itemsApi = testContext.ApiContext.Items;

        [When(@"I get all the items")]
        public async Task WhenIGetAllTheItems() => await _itemsApi.GetAll();
    }
}

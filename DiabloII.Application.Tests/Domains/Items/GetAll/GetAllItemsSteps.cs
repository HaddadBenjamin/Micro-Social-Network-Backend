using System.Threading.Tasks;
using DiabloII.Application.Tests.Domains.Items;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Domains.Suggestions.GetAll
{
    [Binding]
    [Scope(Tag = "items")]
    public class GetAllItemsSteps
    {
        private readonly ItemsApi _itemsApi;

        public GetAllItemsSteps(MyTestContext testContext) => _itemsApi = testContext.Apis.Items;

        [When(@"I get all the items")]
        public async Task WhenIGetAllTheItems() =>
            await _itemsApi.GetAll();
    }
}

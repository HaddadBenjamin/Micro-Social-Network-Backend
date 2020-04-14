using System.Threading.Tasks;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Tests.Contexts;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Items.Search
{
    [Binding]
    [Scope(Tag = "items")]
    public class SearchItemsSteps
    {
        private readonly ItemsApi _itemsApi;

        public SearchItemsSteps(TestContext testContext) => _itemsApi = testContext.ApiContext.Items;

        [When(@"I search the items")]
        public async Task WhenISearchTheItems(Table table)
        {
            var dto = table.CreateInstance<SearchUniquesDto>();

            await _itemsApi.Search(dto);
        }
    }
}

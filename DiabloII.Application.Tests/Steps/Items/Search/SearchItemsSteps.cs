using System.Threading.Tasks;
using DiabloII.Application.Requests.Read.Domains.Items;
using DiabloII.Application.Tests.Apis.Domains.Items;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Items.Search
{
    [Binding]
    [Scope(Tag = "items")]
    public class SearchItemsSteps
    {
        private readonly IItemsApi _itemsApi;

        public SearchItemsSteps(IItemsApi itemsApi) => _itemsApi = itemsApi;

        [When(@"I search the items")]
        public async Task WhenISearchTheItems(Table table)
        {
            var dto = table.CreateInstance<SearchUniquesDto>();

            await _itemsApi.Search(dto);
        }
    }
}

using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Items;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Items.GetAll
{
    [Binding]
    [Scope(Tag = "items")]
    public class GetAllItemsSteps
    {
        private readonly IItemsApi _itemsApi;

        public GetAllItemsSteps(IItemsApi itemsApi) => _itemsApi = itemsApi;

        [When(@"I get all the items")]
        public async Task WhenIGetAllTheItems() => await _itemsApi.GetAll();
    }
}

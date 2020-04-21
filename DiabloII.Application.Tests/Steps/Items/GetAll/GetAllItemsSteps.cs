using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Items;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Items.GetAll
{
    [Binding]
    [Scope(Tag = "items")]
    public class GetAllItemsSteps
    {
        private readonly IItems _items;

        public GetAllItemsSteps(IItems items) => _items = items;

        [When(@"I get all the items")]
        public async Task WhenIGetAllTheItems() => await _items.GetAll();
    }
}

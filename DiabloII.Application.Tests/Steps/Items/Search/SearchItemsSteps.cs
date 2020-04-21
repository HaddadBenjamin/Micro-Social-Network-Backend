using System.Threading.Tasks;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Tests.Apis.Domains.Items;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Items.Search
{
    [Binding]
    [Scope(Tag = "items")]
    public class SearchItemsSteps
    {
        private readonly IItems _items;

        public SearchItemsSteps(IItems items) => _items = items;

        [When(@"I search the items")]
        public async Task WhenISearchTheItems(Table table)
        {
            var dto = table.CreateInstance<SearchUniquesDto>();

            await _items.Search(dto);
        }
    }
}

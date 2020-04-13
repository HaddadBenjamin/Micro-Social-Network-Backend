﻿using System.Threading.Tasks;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Tests.Domains.Items;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Domains.Suggestions.GetAll
{
    [Binding]
    [Scope(Tag = "items")]
    public class SearchItemsSteps
    {
        private readonly ItemsApi _itemsApi;

        public SearchItemsSteps(MyTestContext testContext) => _itemsApi = testContext.Apis.Items;

        [When(@"I search the items")]
        public async Task WhenISearchTheItems(Table table)
        {
            var dto = table.CreateInstance<SearchUniquesDto>();

            await _itemsApi.Search(dto);
        }
    }
}

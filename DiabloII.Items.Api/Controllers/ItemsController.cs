using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Services.Items;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DiabloII.Items.Api.Queries;
using DiabloII.Items.Api.Requests.Items;

namespace DiabloII.Items.Api.Controllers
{
    // Remember : dotnet run watch.
    [Route("api/v1/[controller]")]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService) => _itemsService = itemsService;

        [Route("getalluniques")]
        [HttpGet]
        public IReadOnlyCollection<Item> GetAllUniques() => _itemsService.GetAllUniques();

        [Route("searchuniques")]
        [HttpGet]
        public IReadOnlyCollection<Item> SearchUniques(SearchUniquesDto searchDto) => _itemsService.SearchUniques(new SearchUniquesQuery(searchDto));
    }
}

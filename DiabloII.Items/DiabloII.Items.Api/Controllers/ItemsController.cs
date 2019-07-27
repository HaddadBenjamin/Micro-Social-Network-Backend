using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DiabloII.Items.Api.Items.Responses;
using DiabloII.Items.Api.Items.Queries;
using DiabloII.Items.Api.Items.Services;

namespace DiabloII.Items.Api.Controllers
{
    // Remember : dotnet run watch.
    [Route("api/v1/[controller]")]
    public class ItemsController : Controller
    {
        private readonly IItemsService ItemsService;

        public ItemsController(IItemsService itemsService) => ItemsService = itemsService;

        // GET api/v1/getallhuniques
        [Route("getalluniques")]
        [HttpGet]
        public IEnumerable<Item> SearchUniques() => ItemsService.GetAllUniques();

        // GET api/v1/searchuniques
        [Route("searchuniques")]
        [HttpGet]
        public IEnumerable<Item> SearchUniques(SearchUniquesDto searchDto = default(SearchUniquesDto))
            => ItemsService.SearchUniques(searchDto);
    }
}

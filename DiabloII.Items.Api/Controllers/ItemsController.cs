using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiabloII.Items.Api.Queries.Items;
using DiabloII.Items.Api.Responses.Items;
using DiabloII.Items.Api.Services.Items;
using Microsoft.AspNetCore.Cors;

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
        public async Task<IEnumerable<Item>> GetAllUniques() => await _itemsService.GetAllUniques().ConfigureAwait(false);

        [Route("searchuniques")]
        [HttpGet]
        public async Task<IEnumerable<Item>> SearchUniques(SearchUniquesDto searchDto = default)
            => await _itemsService.SearchUniques(searchDto).ConfigureAwait(false);
    }
}

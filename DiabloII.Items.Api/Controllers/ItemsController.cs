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
    [EnableCors("AllowOrigin")]
    public class ItemsController : Controller
    {
        private readonly IItemsService ItemsService;

        public ItemsController(IItemsService itemsService) => ItemsService = itemsService;

        [Route("getalluniques")]
        [HttpGet]
        public async Task<IEnumerable<Item>> GetAllUniques() => await ItemsService.GetAllUniques().ConfigureAwait(false);

        [Route("searchuniques")]
        [HttpGet]
        public async Task<IEnumerable<Item>> SearchUniques([FromBody] SearchUniquesDto searchDto = default)
            => await ItemsService.SearchUniques(searchDto).ConfigureAwait(false);
    }
}

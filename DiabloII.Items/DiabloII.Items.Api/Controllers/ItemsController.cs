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
        /// <summary>
        /// Get all uniques items.
        /// </summary>
        /// <returns>A all uniques itesm/returns>
        [Route("getalluniques")]
        [HttpGet]
        public IEnumerable<Item> SearchUniques() => ItemsService.GetAllUniques();

        /// <summary>
        /// Seartch uniques items by a different filters.
        /// </summary>
        /// <param name="searchDto"></param>
        [Route("searchuniques")]
        [HttpGet]
        public IEnumerable<Item> SearchUniques(SearchUniquesDto searchDto = default(SearchUniquesDto))
            => ItemsService.SearchUniques(searchDto);

        // TODO : la partie description de mon API ne semble pas fonctionner, il faudra que je google ça un petit peu :)
    }
}

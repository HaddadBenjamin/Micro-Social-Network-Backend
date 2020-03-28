using DiabloII.Items.Api.Services.Items;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.Mappers.ItemDtoMapper;
using DiabloII.Items.Api.Queries;
using DiabloII.Items.Api.Requests.Items;
using DiabloII.Items.Api.Responses.Items;

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
        public IReadOnlyCollection<ItemDto> GetAllUniques() => _itemsService
            .GetAllUniques()
            .Select(ItemDtoMapper.ToItemDto)
            .ToList();

        [Route("searchuniques")]
        [HttpGet]
        public IReadOnlyCollection<ItemDto> SearchUniques(SearchUniquesDto searchDto) => _itemsService
            .SearchUniques(new SearchUniquesQuery(searchDto))
            .Select(ItemDtoMapper.ToItemDto)
            .ToList();
    }
}

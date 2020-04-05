using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Requests.Items;
using DiabloII.Items.Api.Application.Responses.Items;
using DiabloII.Items.Api.Application.Services.Items;
using DiabloII.Items.Api.Domain.Queries.Items;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Items.Api.Application.Controllers
{
    // Remember : dotnet run watch.
    [Route("api/v1/[controller]")]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;
        private readonly IMapper _mapper;

        public ItemsController(IItemsService itemsService, IMapper mapper)
        {
            _itemsService = itemsService;
            _mapper = mapper;
        }

        #region Read
        [Route("getalluniques")]
        [HttpGet]
        public IReadOnlyCollection<ItemDto> GetAllUniques() => _itemsService
            .GetAllUniques()
            .Select(_mapper.Map<ItemDto>)
            .ToList();

        [Route("searchuniques")]
        [HttpGet]
        public IReadOnlyCollection<ItemDto> SearchUniques(SearchUniquesDto searchDto) => _itemsService
            .SearchUniques(_mapper.Map<SearchUniquesQuery>(searchDto))
            .Select(_mapper.Map<ItemDto>)
            .ToList();
        #endregion
    }
}

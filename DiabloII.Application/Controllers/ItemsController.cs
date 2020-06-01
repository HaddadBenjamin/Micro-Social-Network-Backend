using AutoMapper;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Items;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    public class ItemsController : BaseController<Item, ItemDto>
    {
        private readonly IItemReader _reader;

        private readonly IMapper _mapper;

        public ItemsController(IItemReader reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the uniques items
        /// </summary>
        [Route("items")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<ItemDto>), StatusCodes.Status200OK)]
        public ActionResult<ApiResponses<ItemDto>> GetAllUniques() =>
            GetAll(_reader, _mapper);

        /// <summary>
        /// Search the uniques items
        /// </summary>
        [Route("items/search")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<ItemDto>), StatusCodes.Status200OK)]
        public ActionResult<ApiResponses<ItemDto>> SearchUniques(SearchUniquesDto dto) =>
            Search(dto, _reader, _mapper);
    }
}

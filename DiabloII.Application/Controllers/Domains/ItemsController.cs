using AutoMapper;
using DiabloII.Application.Controllers.Bases;
using DiabloII.Application.Requests.Read.Domains.Items;
using DiabloII.Application.Responses;
using DiabloII.Application.Responses.Items;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Readers.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers.Domains
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

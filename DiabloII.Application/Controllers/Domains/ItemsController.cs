using AutoMapper;
using DiabloII.Application.Controllers.Bases;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Items;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Readers.Domains;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers.Domains
{
    [Route("api/v1/")]
    public class ItemsController : BaseController<Item, ItemDto>
    {
        private readonly IItemReader _reader;

        public ItemsController(IItemReader reader, IMapper mapper, IMediator mediator) : base(mediator, mapper) =>
            _reader = reader;

        /// <summary>
        /// Get all the uniques items
        /// </summary>
        [Route("items")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<ItemDto>), StatusCodes.Status200OK)]
        public ActionResult<ApiResponses<ItemDto>> GetAllUniques() =>
            GetAll(_reader);

        /// <summary>
        /// Search the uniques items
        /// </summary>
        [Route("items/search")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<ItemDto>), StatusCodes.Status200OK)]
        public ActionResult<ApiResponses<ItemDto>> SearchUniques(SearchUniquesDto dto) =>
            Search(dto, _reader);
    }
}

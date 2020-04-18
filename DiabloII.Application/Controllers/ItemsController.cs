using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Items;
using DiabloII.Domain.Queries.Items;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    // Remember : dotnet run watch.
    [Route("api/v1/")]
    public class ItemsController : Controller
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
        [ProducesResponseType(typeof(IReadOnlyCollection<ItemDto>), StatusCodes.Status200OK)]
        public ActionResult<IReadOnlyCollection<ItemDto>> GetAllUniques()
        {
            var response = _reader
                .GetAllUniques()
                .Select(_mapper.Map<ItemDto>)
                .ToList();

            return Ok(response);
        }

        /// <summary>
        /// Search the uniques items
        /// </summary>
        [Route("items/search")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<ItemDto>), StatusCodes.Status200OK)]
        public ActionResult<IReadOnlyCollection<ItemDto>> SearchUniques(SearchUniquesDto searchDto)
        {
            var response = _reader
                .SearchUniques(_mapper.Map<SearchUniquesQuery>(searchDto))
                .Select(_mapper.Map<ItemDto>)
                .ToList();

            return Ok(response);
        }
    }
}

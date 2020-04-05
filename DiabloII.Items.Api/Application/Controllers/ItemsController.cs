using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Requests.Items;
using DiabloII.Items.Api.Application.Responses.Items;
using DiabloII.Items.Api.Domain.Queries.Items;
using DiabloII.Items.Api.Domain.Readers;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Items.Api.Application.Controllers
{
    // Remember : dotnet run watch.
    [Route("api/v1/[controller]")]
    public class ItemsController : Controller
    {
        private readonly IItemReader _reader;
        private readonly IMapper _mapper;

        public ItemsController(IItemReader reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        [Route("getalluniques")]
        [HttpGet]
        public IReadOnlyCollection<ItemDto> GetAllUniques() => _reader
            .GetAllUniques()
            .Select(_mapper.Map<ItemDto>)
            .ToList();

        [Route("searchuniques")]
        [HttpGet]
        public IReadOnlyCollection<ItemDto> SearchUniques(SearchUniquesDto searchDto) => _reader
            .SearchUniques(_mapper.Map<SearchUniquesQuery>(searchDto))
            .Select(_mapper.Map<ItemDto>)
            .ToList();
    }
}

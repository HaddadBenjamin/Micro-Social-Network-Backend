﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Items;
using DiabloII.Domain.Queries.Items;
using DiabloII.Domain.Readers;
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

        [Route("items")]
        [HttpGet]
        public ActionResult<IReadOnlyCollection<ItemDto>> GetAllUniques()
        {
            var responseDto = _reader
                .GetAllUniques()
                .Select(_mapper.Map<ItemDto>)
                .ToList();

            return Ok(responseDto);
        }

        [Route("items/search")]
        [HttpGet]
        public ActionResult<IReadOnlyCollection<ItemDto>> SearchUniques(SearchUniquesDto searchDto)
        {
            var responseDto = _reader
                .SearchUniques(_mapper.Map<SearchUniquesQuery>(searchDto))
                .Select(_mapper.Map<ItemDto>)
                .ToList();

            return Ok(responseDto);
        }
    }
}

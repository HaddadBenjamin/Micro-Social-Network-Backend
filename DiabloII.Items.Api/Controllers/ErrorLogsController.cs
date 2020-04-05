using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorLogsController : Controller
    {
        private readonly IErrorLogReader _reader;
        private readonly IMapper _mapper;

        public ErrorLogsController(IErrorLogReader reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<ErrorLogDto> GetAll() => _reader
            .GetAll()
            .Select(_mapper.Map<ErrorLogDto>)
            .ToList();
    }
}
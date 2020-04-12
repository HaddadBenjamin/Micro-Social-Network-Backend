using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
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

        [Route("errorlogs")]
        [HttpGet]
        public ActionResult<IReadOnlyCollection<ErrorLogDto>> GetAll()
        {
            var responseDto = _reader
                .GetAll()
                .Select(_mapper.Map<ErrorLogDto>)
                .ToList();

            return Ok(responseDto);
        }
    }
}
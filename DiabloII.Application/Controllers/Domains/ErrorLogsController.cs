using AutoMapper;
using DiabloII.Application.Controllers.Bases;
using DiabloII.Application.Responses;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Readers.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers.Domains
{
    [Route("api/v1/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorLogsController : BaseController<ErrorLog, ErrorLogDto>
    {
        private readonly IErrorLogReader _reader;

        private readonly IMapper _mapper;

        public ErrorLogsController(IErrorLogReader reader, IMapper mapper)
        {
            _reader = reader;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the error logs.
        /// </summary>
        [ProducesResponseType(typeof(ApiResponses<ErrorLogDto>), StatusCodes.Status200OK)]
        [Route("errorlogs")]
        [HttpGet]
        public ActionResult<ApiResponses<ErrorLogDto>> GetAll() =>
            GetAll(_reader, _mapper);
    }
}
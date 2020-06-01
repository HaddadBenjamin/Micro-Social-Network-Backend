using AutoMapper;
using DiabloII.Application.Controllers.Bases;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.ErrorLogs;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Readers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers.Domains
{
    [Route("api/v1/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorLogsController : BaseController<ErrorLog, ErrorLogDto>
    {
        private readonly IErrorLogReader _reader;

        public ErrorLogsController(IErrorLogReader reader, IMapper mapper, IMediator mediator) : base(mediator, mapper) =>
            _reader = reader;

        /// <summary>
        /// Get all the error logs.
        /// </summary>
        [ProducesResponseType(typeof(ApiResponses<ErrorLogDto>), StatusCodes.Status200OK)]
        [Route("errorlogs")]
        [HttpGet]
        public ActionResult<ApiResponses<ErrorLogDto>> GetAll() =>
            GetAll(_reader);
    }
}
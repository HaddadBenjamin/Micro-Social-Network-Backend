using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Responses.ErrorLogs;
using DiabloII.Items.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Items.Api.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorLogsController : Controller
    {
        private readonly IErrorLogsService _errorLogsService;
        private readonly IMapper _mapper;

        public ErrorLogsController(IErrorLogsService errorLogsService, IMapper mapper)
        {
            _errorLogsService = errorLogsService;
            _mapper = mapper;
        }

        #region Read
        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<ErrorLogDto> GetAll() => _errorLogsService
            .GetAll()
            .Select(_mapper.Map<ErrorLogDto>)
            .ToList();
        #endregion
    }
}
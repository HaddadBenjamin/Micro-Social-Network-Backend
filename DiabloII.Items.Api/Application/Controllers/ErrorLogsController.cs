using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.Application.Mappers.ErrorLogs;
using DiabloII.Items.Api.Application.Responses.ErrorLogs;
using DiabloII.Items.Api.Application.Services.ErrorLogs;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Items.Api.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorLogsController : Controller
    {
        private readonly IErrorLogsService _errorLogsService;

        public ErrorLogsController(IErrorLogsService errorLogsService) => _errorLogsService = errorLogsService;

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<ErrorLogDto> GetAll() => _errorLogsService
            .GetAll()
            .Select(ErrorLogMapper.ToErrorLogDto)
            .ToList();
    }
}
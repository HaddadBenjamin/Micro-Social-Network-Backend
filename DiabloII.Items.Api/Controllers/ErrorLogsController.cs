using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.Suggestions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Items.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorLogsController : Controller
    {
        private readonly IErrorLogsService _errorLogsService;

        public ErrorLogsController(IErrorLogsService errorLogsService) => _errorLogsService = errorLogsService;

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<ErrorLog> GetAll() => _errorLogsService.GetAll();
    }
}
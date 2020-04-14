using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Steps.ErrorLogs
{
    public class ErrorLogsApi
    {
        private readonly HttpContext _httpContext;

        private static readonly string BaseUrl = "errorlogs";

        public ErrorLogsApi(HttpContext httpContext) => _httpContext = httpContext;

        public async Task GetAll() => await _httpContext.GetAsync<IReadOnlyCollection<ErrorLogDto>>(BaseUrl);
    }
}

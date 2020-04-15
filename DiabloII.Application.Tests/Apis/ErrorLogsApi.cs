using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Responses.ErrorLogs;

namespace DiabloII.Application.Tests.Apis
{
    public class ErrorLogsApi
    {
        private readonly HttpContext _httpContext;

        private static readonly string BaseUrl = "errorlogs";

        public ErrorLogsApi(HttpContext httpContext) => _httpContext = httpContext;

        public async Task GetAll() => await _httpContext.GetAsync<IReadOnlyCollection<ErrorLogDto>>(BaseUrl);
    }
}

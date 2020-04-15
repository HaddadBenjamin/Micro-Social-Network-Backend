using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Apis.ErrorLogs
{
    public class ErrorLogsApi : IErrorLogsApi
    {
        private readonly IHttpContext _httpContext;

        private static readonly string BaseUrl = "errorlogs";

        public ErrorLogsApi(IHttpContext httpContext) => _httpContext = httpContext;

        public async Task GetAll() => await _httpContext.GetAsync<IReadOnlyCollection<ErrorLogDto>>(BaseUrl);
    }
}

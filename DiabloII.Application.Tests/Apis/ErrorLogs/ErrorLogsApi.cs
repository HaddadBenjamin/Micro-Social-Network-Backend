using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.ErrorLogs
{
    public class ErrorLogsApi : AApi, IErrorLogsApi
    {
        protected override string BaseUrl { get; } = "errorlogs";

        public ErrorLogsApi(IHttpService httpService) : base(httpService) { }

        public async Task GetAll() => await _httpService.GetAsync<IReadOnlyCollection<ErrorLogDto>>(BaseUrl);
    }
}

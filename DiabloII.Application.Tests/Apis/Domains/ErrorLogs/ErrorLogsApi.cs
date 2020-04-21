using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Domains.ErrorLogs
{
    public class ErrorLogsApi : BaseApi, IErrorLogsApi
    {
        protected override string BaseUrl { get; } = "errorlogs";

        public ErrorLogsApi(IHttpService httpService) : base(httpService) { }

        public async Task<IReadOnlyCollection<ErrorLogDto>> GetAll() => await _httpService.GetAsync<IReadOnlyCollection<ErrorLogDto>>(BaseUrl);
    }
}

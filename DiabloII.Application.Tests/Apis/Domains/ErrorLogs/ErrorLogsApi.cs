using System.Threading.Tasks;
using DiabloII.Application.Responses;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Domains.ErrorLogs;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Domains.ErrorLogs
{
    public class ErrorLogsApi : BaseApi, IErrorLogsApi
    {
        protected override string BaseUrl { get; } = "errorlogs";

        public ErrorLogsApi(IHttpService httpService) : base(httpService) { }

        public async Task<ApiResponses<ErrorLogDto>> GetAll() => await _httpService.GetAsync<ApiResponses<ErrorLogDto>>(BaseUrl);
    }
}

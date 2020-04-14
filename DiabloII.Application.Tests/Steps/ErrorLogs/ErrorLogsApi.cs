using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Steps.ErrorLogs
{
    public class ErrorLogsApi
    {
        private readonly MyHttpClient _httpClient;

        private static readonly string BaseUrl = "errorlogs";

        public ErrorLogsApi(MyHttpClient httpClient) => _httpClient = httpClient;

        public async Task<IReadOnlyCollection<ErrorLogDto>> GetAll() =>
            await _httpClient.GetAsync<IReadOnlyCollection<ErrorLogDto>>(BaseUrl);
    }
}

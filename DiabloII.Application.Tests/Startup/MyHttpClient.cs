using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;

namespace DiabloII.Application.Tests.Startup
{
    public class MyHttpClient
    {
        private readonly FlurlClient _flurlClient;

        public int StatusCode { get; private set; }

        public MyHttpClient(HttpClient httpClient) => _flurlClient = new FlurlClient(httpClient);

        public async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            var flurlResponse = await _flurlClient
                .Request(endpoint)
                .GetAsync();

            StatusCode = flurlResponse.StatusCode;

            return await flurlResponse.GetJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostAsync<TResponse>(string endpoint, object dto)
        {
            var flurlResponse = await _flurlClient
                .Request(endpoint)
                .PostJsonAsync(dto);

            StatusCode = flurlResponse.StatusCode;

            return await flurlResponse.GetJsonAsync<TResponse>();
        }

        public void Dispose() => _flurlClient.Dispose();
    }
}
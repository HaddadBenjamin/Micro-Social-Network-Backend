using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Content;
using Newtonsoft.Json;

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

        public async Task<TResponse> DeleteAsync<TResponse>(string endpoint, object dto)
        {
            var flurlResponse = await _flurlClient
                .Request(endpoint)
                .SendAsync(HttpMethod.Delete, new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"));

            StatusCode = flurlResponse.StatusCode;

            return await flurlResponse.GetJsonAsync<TResponse>();
        }

        public void Dispose() => _flurlClient.Dispose();
    }
}
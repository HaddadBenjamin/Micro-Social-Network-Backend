using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;

namespace DiabloII.Application.Tests.Services.Http
{
    public class HttpService : IDisposable, IHttpService
    {
        private readonly FlurlClient _flurlClient;

        public int StatusCode { get; private set; }

        public HttpService(HttpClient httpClient) => _flurlClient = new FlurlClient(httpClient);

        public async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            var flurlResponse = await GetFlurlRequest(endpoint).GetAsync();

            return await SetStatusCodeAndGetJsonResponse<TResponse>(flurlResponse);
        }

        public async Task<TResponse> PostAsync<TResponse>(string endpoint, object dto)
        {
            var flurlResponse = await GetFlurlRequest(endpoint).PostJsonAsync(dto);

            return await SetStatusCodeAndGetJsonResponse<TResponse>(flurlResponse);
        }

        public async Task<TResponse> DeleteAsync<TResponse>(string endpoint, object dto)
        {
            var flurlResponse = await GetFlurlRequest(endpoint).SendAsync(HttpMethod.Delete, new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"));

            return await SetStatusCodeAndGetJsonResponse<TResponse>(flurlResponse);
        }

        public void Dispose() => _flurlClient.Dispose();

        private IFlurlRequest GetFlurlRequest(string endpoint) => _flurlClient.Request(endpoint);

        private async Task<TResponse> SetStatusCodeAndGetJsonResponse<TResponse>(IFlurlResponse flurlResponse)
        {
            StatusCode = flurlResponse.StatusCode;

            return await flurlResponse.GetJsonAsync<TResponse>();
        }
    }
}
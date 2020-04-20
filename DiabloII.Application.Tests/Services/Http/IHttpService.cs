using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Services.Http
{
    public interface IHttpService
    {
        int StatusCode { get; }

        Task<TResponse> GetAsync<TResponse>(string endpoint);

        Task<TResponse> PostAsync<TResponse>(string endpoint, object dto);

        Task<TResponse> DeleteAsync<TResponse>(string endpoint, object dto);
    }
}
using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Startup
{
    public interface IHttpContext
    {
        int StatusCode { get; }

        Task<TResponse> GetAsync<TResponse>(string endpoint);

        Task<TResponse> PostAsync<TResponse>(string endpoint, object dto);

        Task<TResponse> DeleteAsync<TResponse>(string endpoint, object dto);
    }
}
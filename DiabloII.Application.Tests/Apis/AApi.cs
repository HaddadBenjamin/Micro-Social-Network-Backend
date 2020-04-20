using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis
{
    public abstract class AApi
    {
        protected readonly IHttpService _httpService;

        protected abstract string BaseUrl { get; }

        public AApi(IHttpService httpService) => _httpService = httpService;
    }
}
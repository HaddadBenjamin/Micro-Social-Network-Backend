using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public abstract class BaseApi
    {
        protected readonly IHttpService _httpService;

        protected abstract string BaseUrl { get; }

        public BaseApi(IHttpService httpService) => _httpService = httpService;
    }
}
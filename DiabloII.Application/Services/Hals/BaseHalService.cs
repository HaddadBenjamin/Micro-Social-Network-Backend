using System.Net.Http;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace DiabloII.Application.Services.Hals
{
    public class BaseHalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseHalService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        protected HALResponse ToHalResponse(object model)
        {
            var linkUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var halModelConfig = new HALModelConfig
            {
                LinkBase = linkUrl,
                ForceHAL = false
            };

            return new HALResponse(model, halModelConfig);
        }

        protected HALResponse AddLink(HALResponse halResponse, string linkName, HttpMethod httpMethod,
            string subUrl = null)
        {
            var linkUrl = GetLinkUrl(subUrl);

            return halResponse.AddLinks(new Link(linkName, linkUrl, null, httpMethod.ToString()));
        }

        protected string GetLinkUrl(string subUrl = null)
        {
            var baseUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();

            subUrl = subUrl is null ? string.Empty : $"/{subUrl}";

            return $"{baseUrl}{subUrl}";
        }
    }
}
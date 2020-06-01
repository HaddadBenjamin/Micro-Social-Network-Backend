using System.Net.Http;
using System.Text.RegularExpressions;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace DiabloII.Application.Services.Hals.Bases
{
    public class BaseHalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly Regex _baseUrlRegex = new Regex(@".*api/v(\d)");

        public BaseHalService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        protected HALResponse ToHalResponse(object model)
        {
            var linkUrl = GetBaseUrl();
            var halModelConfig = new HALModelConfig
            {
                LinkBase = linkUrl,
                ForceHAL = false
            };
            var halResponse = new HALResponse(model, halModelConfig);

            return halResponse;
        }

        protected void AddLink(HALResponse halResponse, string linkName, HttpMethod httpMethod,
            string subUrl = null)
        {
            var linkUrl = GetLinkUrl(subUrl);
            var link = new Link(linkName, linkUrl, null, httpMethod.ToString());

            halResponse.AddLinks(link);
        }

        protected string GetLinkUrl(string subUrl = null)
        {
            var baseUrl = GetBaseUrl();
            var normalizedSubUrl = subUrl is null ? string.Empty : $"/{subUrl}";

            return $"{baseUrl}{normalizedSubUrl}";
        }

        protected string GetBaseUrl()
        {
            var requestDisplayUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var baseUrlRegexMatch = _baseUrlRegex.Match(requestDisplayUrl);
            var baseUrl = baseUrlRegexMatch.Groups[0].Value;

            return baseUrl;
        }
    }
}
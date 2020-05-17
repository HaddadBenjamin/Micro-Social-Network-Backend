using System.Net.Http;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Extensions
{
    public static class HalResponseExtensions
    {
        public static HALResponse AddLink(this HALResponse halResponse, ControllerBase controller, string linkName, HttpMethod httpMethod,
            string subUrl = null)
        {
            var linkUrl = GetLinkUrl(controller, subUrl);

            return halResponse.AddLinks(new Link(linkName, linkUrl, null, httpMethod.ToString()));
        }

        private static string GetLinkUrl(ControllerBase controller, string subUrl = null)
        {
            var baseUrl = controller.Request.GetDisplayUrl();

            subUrl = subUrl is null ? string.Empty : $"/{subUrl}";

            var url = $"{baseUrl}{subUrl}";

            return url;
        }
    }
}
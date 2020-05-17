using System.Net.Http;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Extensions
{
    public static class HalResponseExtensions
    {
        public static HALResponse AddLink(this HALResponse halResponse, ControllerBase controller, string linkName, HttpMethod httpMethod,
            string subUrl = null)
        {
            var url = controller.GetUrl(subUrl);

            return halResponse.AddLinks(new Link(linkName, url, null, httpMethod.ToString()));
        }
    }
}
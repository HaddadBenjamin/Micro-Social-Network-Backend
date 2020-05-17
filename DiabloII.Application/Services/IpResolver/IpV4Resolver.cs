using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Services.IpResolver
{
    public class IpV4Resolver
    {
        private readonly HttpContext _httpContext;

        public IpV4Resolver(IHttpContextAccessor httpContextAccessor) => _httpContext = httpContextAccessor.HttpContext;

        public string ResolveRequestIp()
        {
            var remoteIpAddress = _httpContext.Connection.RemoteIpAddress;
            var requestIp = remoteIpAddress.IsIPv4MappedToIPv6
                ? remoteIpAddress.MapToIPv4().ToString()
                : remoteIpAddress.ToString();
            var isLocalHost = requestIp == "::1";

            if (isLocalHost)
                requestIp = ResolveMyIp();

            return requestIp;
        }

        public string ResolveMyIp()
        {
            var myIpAddress = string.Empty;
            var webRequest = WebRequest.Create("http://checkip.dyndns.org/");

            using (var webResponse = webRequest.GetResponse())
            using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
                myIpAddress = streamReader.ReadToEnd();

            var ipAddressStartIndex = myIpAddress.IndexOf("Address: ") + 9;
            var ipAddressLastIndex = myIpAddress.LastIndexOf("</body>");

            return myIpAddress.Substring(ipAddressStartIndex, ipAddressLastIndex - ipAddressStartIndex);
        }
    }
}
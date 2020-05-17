using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Services.IpResolver
{
    public class IpV4Resolver : IIpV4Resolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IpV4Resolver(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public string ResolveRequestIp()
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            var remoteIp = remoteIpAddress.IsIPv4MappedToIPv6
                ? remoteIpAddress.MapToIPv4().ToString()
                : remoteIpAddress.ToString();
            var isLocalHost = remoteIp == "::1";
            var requestIp = isLocalHost ? ResolveMyIp() : remoteIp;

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
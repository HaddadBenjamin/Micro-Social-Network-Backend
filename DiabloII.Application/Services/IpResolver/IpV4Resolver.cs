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
            var remoteIpv4 = remoteIpAddress.IsIPv4MappedToIPv6
                ? remoteIpAddress.MapToIPv4().ToString()
                : remoteIpAddress.ToString();
            var remoteIpv4ComeFromLocalHost = remoteIpv4 == "::1";
            var requestIp = remoteIpv4ComeFromLocalHost ? ResolveMyIp() : remoteIpv4;

            return requestIp;
        }

        public string ResolveMyIp()
        {
            var webRequest = WebRequest.Create("http://checkip.dyndns.org/");

            using (var webResponse = webRequest.GetResponse())
            using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                var ipAdressResponse = streamReader.ReadToEnd();
                var ipAddressStartIndex = ipAdressResponse.IndexOf("Address: ") + 9;
                var ipAddressLastIndex = ipAdressResponse.LastIndexOf("</body>");
                var myIpAddress = ipAdressResponse.Substring(ipAddressStartIndex, ipAddressLastIndex - ipAddressStartIndex);

                return myIpAddress;
            }
        }
    }
}
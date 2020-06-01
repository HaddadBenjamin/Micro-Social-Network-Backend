using System.IO;
using System.Net;

namespace DiabloII.Application.Resolvers.Implementations.Ip
{
    public class RequestIpV4Resolver : IRequestIpV4Resolver
    {
        public string Resolve()
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
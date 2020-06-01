using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Resolvers.Implementations.Ip
{
    public class MyIpV4IpResolver : IMyIpV4Resolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMyIpV4Resolver _myIpV4Resolver;

        public MyIpV4IpResolver(IHttpContextAccessor httpContextAccessor, IMyIpV4Resolver myIpV4Resolver)
        {
            _httpContextAccessor = httpContextAccessor;
            _myIpV4Resolver = myIpV4Resolver;
        }

        public string Resolve()
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            var remoteIpv4 = remoteIpAddress.IsIPv4MappedToIPv6
                ? remoteIpAddress.MapToIPv4().ToString()
                : remoteIpAddress.ToString();
            var remoteIpv4ComeFromLocalHost = remoteIpv4 == "::1";
            var requestIp = remoteIpv4ComeFromLocalHost ? _myIpV4Resolver.Resolve() : remoteIpv4;

            return requestIp;
        }
    }
}
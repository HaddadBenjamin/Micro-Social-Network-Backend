using System;
using DiabloII.Application.Helpers;
using DiabloII.Application.Services.IpResolver;

namespace DiabloII.Application.Services.UserIdResolver
{
    public class UserIdResolverService : IUserIdResolverService
    {
        private readonly IIpV4Resolver _ipV4Resolver;

        public UserIdResolverService(IIpV4Resolver ipV4Resolver) => _ipV4Resolver = ipV4Resolver;

        public string Resolve()
        {
            var requestIp = _ipV4Resolver.ResolveRequestIp();
            var userIp = SecurityAlgorithmHelpers.GenerateSha1Hash(requestIp);

            return userIp;
        }
    }
}
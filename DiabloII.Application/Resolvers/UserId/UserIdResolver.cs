using DiabloII.Application.Helpers;
using DiabloII.Application.Resolvers.Ip;

namespace DiabloII.Application.Resolvers.UserId
{
    public class UserIdResolver : IUserIdResolver
    {
        private readonly IRequestIpV4Resolver _requestIpV4Resolver;

        public UserIdResolver(IRequestIpV4Resolver requestIpV4Resolver) => _requestIpV4Resolver = requestIpV4Resolver;

        public string Resolve()
        {
            var requestIpV4 = _requestIpV4Resolver.Resolve();
            var userIp = SecurityAlgorithmHelpers.GenerateSha1Hash(requestIpV4);

            return userIp;
        }
    }
}
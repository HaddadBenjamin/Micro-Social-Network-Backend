using DiabloII.Application.Resolvers.Ip;

namespace DiabloII.Application.Tests.Mocks
{
    public class RequestIpV4ResolverMock : IRequestIpV4Resolver
    {
        public string Resolve() => "94.174.157.14";
    }
}

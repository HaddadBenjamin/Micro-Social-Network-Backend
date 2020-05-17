using System;
using DiabloII.Application.Services.IpResolver;

namespace DiabloII.Application.Tests.Mocks
{
    public class IpV4ResolverMock : IIpV4Resolver
    {
        public string ResolveRequestIp() => "94.174.157.14";

        public string ResolveMyIp() => throw new NotImplementedException();
    }
}

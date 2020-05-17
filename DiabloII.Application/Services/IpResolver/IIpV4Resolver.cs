namespace DiabloII.Application.Services.IpResolver
{
    public interface IIpV4Resolver
    {
        string ResolveRequestIp();

        string ResolveMyIp();
    }
}
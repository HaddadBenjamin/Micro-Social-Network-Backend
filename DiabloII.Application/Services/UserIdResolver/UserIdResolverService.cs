using DiabloII.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Services.UserIdResolver
{
    public class UserIdResolverService : IUserIdResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdResolverService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public string Resolve()
        {
            // Is always ip v4 ?
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            // does the hash algorithm is correct ?
            var userIp = SecurityAlgorithmHelpers.GenerateSha1Hash(ipAddress);

            return userIp;
        }
    }
}
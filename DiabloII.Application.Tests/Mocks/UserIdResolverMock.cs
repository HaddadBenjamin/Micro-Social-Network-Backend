using DiabloII.Application.Resolvers.UserId;

namespace DiabloII.Application.Tests.Mocks
{
    public class UserIdResolverMock : IUserIdResolver
    {
        private readonly string _userId;

        public UserIdResolverMock(string userId) => _userId = userId;

        public string Resolve() => _userId;
    }
}
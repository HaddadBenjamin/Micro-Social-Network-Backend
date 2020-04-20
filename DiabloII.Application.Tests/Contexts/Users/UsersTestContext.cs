using DiabloII.Application.Responses.Users;

namespace DiabloII.Application.Tests.Contexts.Users
{
    public class UsersTestContext : IUsersTestContext
    {
        public UserDto CreatedUser { get; set; }
    }
}
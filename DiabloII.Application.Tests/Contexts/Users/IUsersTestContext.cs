using DiabloII.Application.Responses.Users;

namespace DiabloII.Application.Tests.Contexts.Users
{
    public interface IUsersTestContext
    {
        public UserDto CreatedUser { get; set; }
    }
}

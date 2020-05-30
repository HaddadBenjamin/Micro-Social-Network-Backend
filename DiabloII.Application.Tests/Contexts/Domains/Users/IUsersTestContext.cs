using DiabloII.Application.Responses.Users;
using DiabloII.Application.Tests.Contexts.Bases;

namespace DiabloII.Application.Tests.Contexts.Domains.Users
{
    public interface IUsersTestContext :
        ITestContextAll<UserDto>,
        ITestContextCreated<UserDto>,
        ITestContextUpdated<UserDto>
    {
        UserDto IdentifiedUser { get; set; }
    }
}

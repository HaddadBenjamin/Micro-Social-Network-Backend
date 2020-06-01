using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Users;

namespace DiabloII.Application.Tests.Contexts.Domains.Users
{
    public class UsersTestContext : IUsersTestContext
    {
        public ApiResponses<UserDto> Resources { get; set; }

        public UserDto CreatedResource { get; set; }

        public UserDto UpdatedResource { get; set; }

        public UserDto IdentifiedUser { get; set; }
    }
}
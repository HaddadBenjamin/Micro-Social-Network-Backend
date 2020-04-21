using System.Collections.Generic;
using DiabloII.Application.Responses.Users;

namespace DiabloII.Application.Tests.Contexts.Domains.Users
{
    public class UsersTestContext : IUsersTestContext
    {
        public IReadOnlyCollection<UserDto> AllResources { get; set; }
        
        public UserDto CreatedResource { get; set; }
      
        public UserDto UpdatedResource { get; set; }
    }
}
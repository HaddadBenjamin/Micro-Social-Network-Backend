﻿using System.Collections.Generic;
using DiabloII.Application.Responses.Users;

namespace DiabloII.Application.Tests.Contexts.Users
{
    public class UsersTestContext : IUsersTestContext
    {
        public IReadOnlyCollection<UserDto> AllUsers { get; set; }

        public UserDto CreatedUser { get; set; }

        public UserDto UpdatedUser { get; set; }
    }
}
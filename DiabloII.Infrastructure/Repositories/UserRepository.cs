﻿using System.Linq;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public bool DoesUserExists(string userId) => _dbContext.Users.Any(user => user.Id == userId);
    }
}
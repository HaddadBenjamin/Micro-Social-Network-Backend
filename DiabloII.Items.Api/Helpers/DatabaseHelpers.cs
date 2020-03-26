using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DiabloII.Items.Api.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiabloII.Items.Api.Helpers
{
    public static class DatabaseHelpers
    {
        public static string GetApplicationDbContextConnectionString(IConfiguration configuration, string applicationName = "Diablo II Documentation")
        {
            var dbUsername = configuration["connectionstrings:documentation:username"];
            var dbPassword = configuration["connectionstrings:documentation:password"];
            var dbConnection = configuration.GetConnectionString("Documentation");
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbConnection)
            {
                UserID = dbUsername,
                Password = dbPassword,
                ApplicationName = applicationName,
            };

            return sqlConnectionStringBuilder.ConnectionString;
        }

        public static ApplicationDbContext CreateATestDatabase(string databaseName = "TestDatabase")
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureDeleted();

            return dbContext;
        }

        public static ApplicationDbContext GetTheDatabase(string connectionString)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var dbContext = new ApplicationDbContext(options);

            return dbContext;
        }
    }
}

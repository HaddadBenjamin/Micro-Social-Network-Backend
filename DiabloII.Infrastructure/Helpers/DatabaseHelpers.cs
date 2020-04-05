using System.Data.SqlClient;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiabloII.Infrastructure.Helpers
{
    public static class DatabaseHelpers
    {
        public static string GetMyConnectionString(IConfiguration configuration, string applicationName = "Diablo II Documentation")
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

        public static ApplicationDbContext GetMyDbContext(string connectionString)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            return dbContext;
        }
    }
}

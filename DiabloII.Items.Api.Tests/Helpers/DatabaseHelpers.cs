using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.Tests.Helpers
{
    public static class DatabaseHelpers
    {
        public static ApplicationDbContext CreateMyTestDbContext(string databaseName = "TestDatabase")
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var dbContext = new ApplicationDbContext(options);

            dbContext.Database.EnsureDeleted();

            return dbContext;
        }
    }
}

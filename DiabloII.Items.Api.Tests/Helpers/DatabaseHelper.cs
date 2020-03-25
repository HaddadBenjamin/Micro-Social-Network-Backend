using DiabloII.Items.Api.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.Tests.Helpers
{
    public static class DatabaseHelper
    {
        public static ApplicationDbContext CreateATestDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DocumentationTest")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            
            dbContext.Database.EnsureDeleted();

            return dbContext;
        }
    }
}
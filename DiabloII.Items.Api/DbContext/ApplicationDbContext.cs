using DiabloII.Items.Api.DbContext.ErrorLogs.Models;
using DiabloII.Items.Api.DbContext.Items;
using DiabloII.Items.Api.DbContext.Items.Mappers;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.DbContext.Suggestions.Mappers;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Suggestion> Suggestions { get; set; }

        public DbSet<SuggestionVote> SuggestionVotes { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public DbSet<Item> Items{ get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SuggestionDbMapper.Map(modelBuilder);
            SuggestionVoteDbMapper.Map(modelBuilder);

            ItemDbMapper.Map(modelBuilder);
            ItemPropertyDbMapper.Map(modelBuilder);
        }
    }
}

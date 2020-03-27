using DiabloII.Items.Api.DbContext.DbMappers;
using DiabloII.Items.Api.DbContext.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Suggestion> Suggestions { get; set; }

        public DbSet<SuggestionVote> SuggestionVotes { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SuggestionDbMapper.Map(modelBuilder);
            SuggestionVoteDbMapper.Map(modelBuilder);
        }
    }
}

using DiabloII.Items.Api.DbContext.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.DbContext.DbMappers
{
    public static class SuggestionVoteDbMapper
    {
        private static readonly int IPV4_LENGTH = 15;

        public static void Map(ModelBuilder modelBuilder)
        {
            var suggestionVoteBuilder = modelBuilder.Entity<SuggestionVote>();
            
            suggestionVoteBuilder.HasKey(suggestionVote => suggestionVote.Id);
            suggestionVoteBuilder.HasKey(suggestionVote => new { suggestionVote.SuggestionId, suggestionVote.Ip});
            
            suggestionVoteBuilder
                .HasOne(suggestionVote => suggestionVote.Suggestion)
                .WithMany(suggestionVote => suggestionVote.Votes)
                .HasForeignKey(suggestionVote => suggestionVote.SuggestionId);
            
            suggestionVoteBuilder
                .Property(suggestionVote => suggestionVote.Ip)
                .IsRequired()
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(IPV4_LENGTH);
        }
    }
}
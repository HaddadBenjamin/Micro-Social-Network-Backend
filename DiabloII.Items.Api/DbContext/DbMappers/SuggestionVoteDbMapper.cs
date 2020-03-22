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
            suggestionVoteBuilder
                .Property(suggestionVote => suggestionVote.Id)
                .ValueGeneratedOnAdd();

            suggestionVoteBuilder.HasKey(suggestionVote => new { suggestionVote.SuggestionId, suggestionVote.Ip});
            
            suggestionVoteBuilder
                .HasOne(suggestionVote => suggestionVote.Suggestion)
                .WithMany(suggestionVote => suggestionVote.Votes)
                .HasForeignKey(suggestionVote => suggestionVote.SuggestionId);

            suggestionVoteBuilder.Ignore(suggestionVote => suggestionVote.Suggestion);
            
            suggestionVoteBuilder
                .Property(suggestionVote => suggestionVote.Ip)
                .IsRequired()
                .HasMaxLength(IPV4_LENGTH);
        }
    }
}
using DiabloII.Domain.Models.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Suggestions
{
    public static class SuggestionVoteDbMapper
    {
        private static readonly int Ipv4Length = 15;

        public static void Map(ModelBuilder modelBuilder)
        {
            var suggestionVoteBuilder = modelBuilder.Entity<SuggestionVote>();
            
            suggestionVoteBuilder.HasKey(suggestionVote => suggestionVote.Id);
            suggestionVoteBuilder
                .HasIndex(suggestion => suggestion.Id)
                .IsUnique();

            suggestionVoteBuilder
                .HasOne(suggestionVote => suggestionVote.Suggestion)
                .WithMany(suggestionVote => suggestionVote.Votes)
                .HasForeignKey(suggestionVote => suggestionVote.SuggestionId);

            suggestionVoteBuilder.Ignore(suggestionVote => suggestionVote.Suggestion);
            
            suggestionVoteBuilder
                .Property(suggestionVote => suggestionVote.Ip)
                .IsRequired()
                .HasMaxLength(Ipv4Length);
        }
    }
}
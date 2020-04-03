using DiabloII.Items.Api.DbContext.Suggestions.Models;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.DbContext.Suggestions.Mappers
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

            suggestionVoteBuilder.HasKey(suggestionVote => new { suggestionVote.Suggestion.Id, suggestionVote.Ip});
            
            suggestionVoteBuilder
                .HasOne(suggestionVote => suggestionVote.Suggestion)
                .WithMany(suggestionVote => suggestionVote.Votes)
                .HasForeignKey(suggestionVote => suggestionVote.Suggestion.Id);

            suggestionVoteBuilder.Ignore(suggestionVote => suggestionVote.Suggestion);
            
            suggestionVoteBuilder
                .Property(suggestionVote => suggestionVote.Ip)
                .IsRequired()
                .HasMaxLength(Ipv4Length);
        }
    }

    public static class SuggestionCommentDbMapper
    {
        private static readonly int Ipv4Length = 15;

        public static void Map(ModelBuilder modelBuilder)
        {
            var suggestionVoteBuilder = modelBuilder.Entity<SuggestionComment>();

            suggestionVoteBuilder.HasKey(suggestionVote => suggestionVote.Id);
            suggestionVoteBuilder
                .HasIndex(suggestion => suggestion.Id)
                .IsUnique();

            suggestionVoteBuilder.HasKey(suggestionVote => new { suggestionVote.Suggestion.Id, suggestionVote.Ip });

            suggestionVoteBuilder
                .HasOne(suggestionVote => suggestionVote.Suggestion)
                .WithMany(suggestionVote => suggestionVote.Votes)
                .HasForeignKey(suggestionVote => suggestionVote.Suggestion.Id);

            suggestionVoteBuilder.Ignore(suggestionVote => suggestionVote.Suggestion);

            suggestionVoteBuilder
                .Property(suggestionVote => suggestionVote.Ip)
                .IsRequired()
                .HasMaxLength(Ipv4Length);
        }
    }
}
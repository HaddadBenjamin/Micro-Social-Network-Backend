using DiabloII.Items.Api.Domain.Models.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.Infrastructure.DbContext.Mappers.Suggestions
{
    public static class SuggestionCommentDbMapper
    {
        private static readonly int Ipv4Length = 15;

        public static void Map(ModelBuilder modelBuilder)
        {
            var suggestionVoteBuilder = modelBuilder.Entity<SuggestionComment>();

            suggestionVoteBuilder.HasKey(suggestionComment => suggestionComment.Id);
            suggestionVoteBuilder
                .HasIndex(suggestion => suggestion.Id)
                .IsUnique();

            suggestionVoteBuilder
                .HasOne(suggestionComment => suggestionComment.Suggestion)
                .WithMany(suggestion => suggestion.Comments)
                .HasForeignKey(suggestionComment => suggestionComment.SuggestionId);

            suggestionVoteBuilder.Ignore(suggestionComment => suggestionComment.Suggestion);

            suggestionVoteBuilder
                .Property(suggestionComment => suggestionComment.Ip)
                .IsRequired()
                .HasMaxLength(Ipv4Length);

            suggestionVoteBuilder.Property(suggestionComment => suggestionComment.Comment)
                .IsRequired();
        }
    }
}
using DiabloII.Domain.Models.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Suggestions
{
    public static class SuggestionCommentDbMapper
    {
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
                .Property(suggestionComment => suggestionComment.CreatedBy)
                .IsRequired();

            suggestionVoteBuilder.Property(suggestionComment => suggestionComment.Comment)
                .IsRequired();
        }
    }
}
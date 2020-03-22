using DiabloII.Items.Api.DbContext.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.DbContext.DbMappers
{
    public static class SuggestionDbMapper
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var suggestionBuilder = modelBuilder.Entity<Suggestion>();

            suggestionBuilder.HasKey(suggestion => suggestion.Id);
            suggestionBuilder
                .HasIndex(suggestion => suggestion.Content)
                .IsUnique();

            suggestionBuilder
                .HasMany(suggestion => suggestion.Votes)
                .WithOne(suggestion => suggestion.Suggestion)
                .IsRequired();
            
            suggestionBuilder.Property(suggestion => suggestion.Content).IsRequired();
        }
    }
}
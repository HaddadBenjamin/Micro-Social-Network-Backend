﻿using DiabloII.Domain.Models.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Suggestions
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
                .HasIndex(suggestion => suggestion.Content)
                .IsUnique();

            suggestionBuilder
                .HasMany(suggestion => suggestion.Votes)
                .WithOne(suggestion => suggestion.Suggestion)
                .IsRequired();

            suggestionBuilder
                .HasMany(suggestion => suggestion.Comments)
                .WithOne(suggestion => suggestion.Suggestion)
                .IsRequired();

            suggestionBuilder.Property(suggestion => suggestion.Content).IsRequired();
        }
    }
}
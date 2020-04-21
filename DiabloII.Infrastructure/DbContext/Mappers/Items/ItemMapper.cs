using System;
using DiabloII.Domain.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Items
{
    public static class ItemMapper
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<Item>();

            itemBuilder.HasKey(item => item.Id);
            itemBuilder
                .HasIndex(item => item.Id)
                .IsUnique();

            itemBuilder
                .HasMany(item => item.Properties)
                .WithOne(item => item.Item)
                .IsRequired();

            itemBuilder
                .Property(item => item.Quality)
                .HasConversion(
                    itemQuality => itemQuality.ToString(),
                    itemQuality => (ItemQuality)Enum.Parse(typeof(ItemQuality), itemQuality));
            itemBuilder
                .Property(item => item.Category)
                .HasConversion(
                    itemCategory => itemCategory.ToString(),
                    itemcCategory => (ItemCategory)Enum.Parse(typeof(ItemCategory), itemcCategory));
        }
    }
}
using DiabloII.Items.Api.DbContext.Items.Models;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.DbContext.Items.Mappers
{
    public static class ItemPropertyDbMapper
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var itemPropertyBuilder = modelBuilder.Entity<ItemProperty>();

            itemPropertyBuilder.HasKey(itemProperty => itemProperty.Id);
            itemPropertyBuilder
                .HasIndex(itemProperty => itemProperty.Id)
                .IsUnique();

            itemPropertyBuilder
                .HasOne(itemProperty => itemProperty.Item)
                .WithMany(itemProperty => itemProperty.Properties)
                .HasForeignKey(itemProperty => itemProperty.ItemId);
        }
    }
}
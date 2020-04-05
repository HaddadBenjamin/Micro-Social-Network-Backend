using DiabloII.Domain.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Items
{
    public static class ItemPropertyMapper
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
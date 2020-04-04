using System.Linq;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Responses.Items;

namespace DiabloII.Items.Api.Mappers.Items
{
    public static class ItemMapper
    {
        public static ItemDto ToItemDto(Item item) => new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            ImageName = item.ImageName,

            Quality = item.Quality.ToString(),
            Category = item.Category.ToString(),
            SubCategory = item.SubCategory,
            Type = item.Type,

            LevelRequired = item.LevelRequired,
            Level = item.Level,

            Properties = item.Properties
                .Select(ToItemPropertyDto)
                .ToList(),

            MinimumDefenseMinimum = item.MinimumDefenseMinimum,
            MaximumDefenseMinimum = item.MaximumDefenseMinimum,
            MinimumDefenseMaximum = item.MinimumDefenseMaximum,
            MaximumDefenseMaximum = item.MaximumDefenseMaximum,

            MinimumOneHandedDamageMinimum = item.MinimumOneHandedDamageMinimum,
            MaximumOneHandedDamageMinimum = item.MaximumOneHandedDamageMinimum,
            MinimumTwoHandedDamageMinimum = item.MinimumTwoHandedDamageMinimum,
            MaximumTwoHandedDamageMinimum = item.MaximumTwoHandedDamageMinimum,
            MinimumOneHandedDamageMaximum = item.MinimumOneHandedDamageMaximum,
            MaximumOneHandedDamageMaximum = item.MaximumOneHandedDamageMaximum,
            MinimumTwoHandedDamageMaximum = item.MinimumTwoHandedDamageMaximum,
            MaximumTwoHandedDamageMaximum = item.MaximumTwoHandedDamageMaximum,

            StrengthRequired = item.StrengthRequired,
            DexterityRequired = item.DexterityRequired
        };

        private static ItemPropertyDto ToItemPropertyDto(ItemProperty itemProperty) => new ItemPropertyDto
        {
            Id = itemProperty.Id,

            FormattedName = itemProperty.FormattedName,
            Name = itemProperty.Name,

            Par = itemProperty.Par,
            Minimum = itemProperty.Minimum,
            Maximum = itemProperty.Maximum,
            IsPercent = itemProperty.IsPercent,

            FirstChararacter = itemProperty.FirstChararacter,
            OrderIndex = itemProperty.OrderIndex,
        };
    }
}
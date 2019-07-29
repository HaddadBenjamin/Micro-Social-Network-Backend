using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiabloII.Items.Api.Items.Queries
{
    // Enum category / sub category
    // rajouter level
    // revoir les filtres
    public class SearchUniquesDto
    {
        /// <summary>
        /// Filter items by it's name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Filter items by it's quality.
        /// </summary>
        [EnumDataType(typeof(ItemQuality))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemQuality? Quality { get; set; }

        /// <summary>
        /// Filter items by it's category.
        /// </summary>
        [EnumDataType(typeof(ItemCategory))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemCategory? Category { get; set; }
        
        /// <summary>
        /// Filter items by it's sub category.
        /// </summary>
        [EnumDataType(typeof(ItemSubCategory))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemSubCategory? SubCategory { get; set; }
      
        /// <summary>
        /// Filter items by it's required level.
        /// </summary>
        public int? LevelRequired { get; set; }
        
        
        public string Type { get; set; }
        /// <summary>
        /// Filter items by it's different properties.
        /// </summary>
        public IEnumerable<string> PropertyNames { get; set; }
    }

    public enum ItemQuality
    {
        Normal,
        Magical,
        Rare,
        Unique,
        Set,
        Crafted
    }

    public enum ItemCategory
    {
        Weapon,
        Armor,
        Jewelry,
        Charm,
    }

    public enum ItemSubCategory
    {
        // Armor
        Armor,
        Glove,
        Belt,
        Boot,
        Shield,
        NecroShield,
        PaladinShield,
        Helm,
        DruidHelm,
        // Weapon
        Bow,
        Crossbow,
        Staff,
        Wand,
        Sword,
        TwoHandedSword,
        Dagger,
        Axe,
        AxeWeapon, // to verify
        Lance,
        Masse,
        Scepter,
        ThrowWeapon,
        Scythe,
        Javelin,
        Claw,
        // Jewelry
        Ring,
        Amulet,
        Jewel,
        // Charm
        Charm,
        Arrow
        // Arrow
    }
}
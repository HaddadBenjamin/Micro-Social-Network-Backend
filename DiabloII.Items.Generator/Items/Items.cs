using System;
using System.Collections.Generic;

namespace DiabloII.Items.Generator.Items
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Quality { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Type { get; set; }
        public double LevelRequired { get; set; }
        public double Level { get; set; }
        public IEnumerable<ItemProperty> Properties { get; set; }
        public string ImageName { get; set; }

        // Specific to Armor.
        public double? MinimumDefenseMinimum { get; set; }
        public double? MaximumDefenseMinimum { get; set; }
        public double? MinimumDefenseMaximum { get; set; }
        public double? MaximumDefenseMaximum { get; set; }

        // Specific to Weapon.
        public double? MinimumOneHandedDamageMinimum { get; set; }
        public double? MaximumOneHandedDamageMinimum { get; set; }
        public double? MinimumTwoHandedDamageMinimum { get; set; }
        public double? MaximumTwoHandedDamageMinimum { get; set; }
        public double? MinimumOneHandedDamageMaximum { get; set; }
        public double? MaximumOneHandedDamageMaximum { get; set; }
        public double? MinimumTwoHandedDamageMaximum { get; set; }
        public double? MaximumTwoHandedDamageMaximum { get; set; }

        // Stat required.
        public double? StrengthRequired { get; set; }
        public double? DexterityRequired { get; set; }
    }
}
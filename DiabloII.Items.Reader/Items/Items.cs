using System;
using System.Collections.Generic;

namespace DiabloII.Items.Reader
{
    public class Item
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
        public string Quality { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Type { get; set; }
		public int LevelRequired { get; set; }
        public int Level { get; set; }
        public IEnumerable<ItemProperty> Properties { get; set; }
		
		// Specific to Armor.
		public int? MinimumDefenseMinimum { get; set; }
		public int? MaximumDefenseMinimum { get; set; }
		public int? MinimumDefenseMaximum { get; set; }
		public int? MaximumDefenseMaximum { get; set; }

		// Specific to Weapon.
		public int? MinimumOneHandedDamageMinimum { get; set; }
		public int? MaximumOneHandedDamageMinimum { get; set; }
		public int? MinimumTwoHandedDamageMinimum { get; set; }
		public int? MaximumTwoHandedDamageMinimum { get; set; }
		public int? MinimumOneHandedDamageMaximum { get; set; }
		public int? MaximumOneHandedDamageMaximum { get; set; }
		public int? MinimumTwoHandedDamageMaximum { get; set; }
		public int? MaximumTwoHandedDamageMaximum { get; set; }

		// Stat required.
		public int? StrengthRequired { get; set; }
		public int? DexterityRequired { get; set; }
	}
}
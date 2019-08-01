using System.Collections.Generic;

namespace DiabloII.Items.Reader
{
    public class Item
    {
        public string Name { get; set; }
        public string Quality { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Type { get; set; }
		public int LevelRequired { get; set; }
        public int Level { get; set; }
        public IEnumerable<ItemProperty> Properties { get; set; }
		
		// Specific to Armor.
		public int? MinimumDefense { get; set; }
		public int? MaximumDefense { get; set; }
		
		// Specific to Weapon.
		public int? MinimumOneHandedDamageMinimum { get; set; }
		public int? MaximumOneHandedDamageMinimum { get; set; }
		public int? MinimumTwoHandedDamageMinimum { get; set; }
		public int? MaximumTwoHandedDamageMinimum { get; set; }
		public int? MinimumOneHandedDamageMaximum { get; set; }
		public int? MaximumOneHandedDamageMaximum { get; set; }
		public int? MinimumTwoHandedDamageMaximum { get; set; }
		public int? MaximumTwoHandedDamageMaximum { get; set; }
		public int? AttackSpeed { get; set; }

		// Stat required.
		public int? StrengthRequired { get; set; }
		public int? DexterityRequired { get; set; }
	}
}
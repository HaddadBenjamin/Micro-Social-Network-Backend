using System.Collections.Generic;

namespace DiabloII.Items.Reader
{
    public class Item
    {
        public string Name { get; set; }
        public string Quality { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int LevelRequired { get; set; }
        public int Level { get; set; }
        public IEnumerable<ItemProperty> Properties { get; set; }
		
		// Specific to Armor.
		public int? MinimumDefense { get; set; }
		public int? MaximumDefense { get; set; }
		
		// Specific to Weapon.
		public int? MinimumOneHandedDamage { get; set; }
		public int? MaximumOneHandedDamage { get; set; }
		public int? MinimumTwoHandedDamage { get; set; }
		public int? MaximumTwoHandedDamage { get; set; }
	}
}
namespace DiabloII.Items.Reader.Records
{
    public class ItemCategoryRecord
    {
        public string Name { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }

		// Specific to Armor.
		public int MinimumDefense { get; set; }
		public int MaximumDefense { get; set; }

		// Specific to Weapon
		public int MinimumOneHandedDamage { get; set; }
		public int MaximumOneHandedDamage { get; set; }
		public int MinimumTwoHandedDamage { get; set; }
		public int MaximumTwoHandedDamage { get; set; }
		public int AttackSpeed { get; set; }

		// Stat required
		public int StrengthRequired { get; set; }
		public int DexterityRequired { get; set; }
	}
}
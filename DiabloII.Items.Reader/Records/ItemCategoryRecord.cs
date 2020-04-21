namespace DiabloII.Items.Reader.Records
{
    public class ItemCategoryRecord
    {
        public string Name { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public string ImageName { get; set; }

        // Specific to Armor.
        public double MinimumDefense { get; set; }
        public double MaximumDefense { get; set; }

        // Specific to Weapon
        public double MinimumOneHandedDamage { get; set; }
        public double MaximumOneHandedDamage { get; set; }
        public double MinimumTwoHandedDamage { get; set; }
        public double MaximumTwoHandedDamage { get; set; }

        // Stat required
        public double StrengthRequired { get; set; }
        public double DexterityRequired { get; set; }
    }
}
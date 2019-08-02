namespace DiabloII.Items.Reader.Records
{
    public class WeaponRecord
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Slot { get; set; }

		public int MinimumOneHandedDamage { get; set; }
		public int MaximumOneHandedDamage { get; set; }
		public int MinimumTwoHandedDamage { get; set; }
		public int MaximumTwoHandedDamage { get; set; }

		public int StrengthRequired { get; set; }
		public int DexterityRequired { get; set; }
	}
}
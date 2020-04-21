namespace DiabloII.Items.Reader.Records
{
    public class WeaponRecord
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Slot { get; set; }
        public string ImageName { get; set; }


        public double MinimumOneHandedDamage { get; set; }
        public double MaximumOneHandedDamage { get; set; }
        public double MinimumTwoHandedDamage { get; set; }
        public double MaximumTwoHandedDamage { get; set; }
        public double StrengthRequired { get; set; }
        public double DexterityRequired { get; set; }
    }
}
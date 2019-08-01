namespace DiabloII.Items.Reader.Records
{
    public class ItemCategoryRecord
    {
        public string Name { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }

		/// <summary>
		/// Specific to Armor.
		/// </summary>
		public int MinimumDefense { get; set; }
		public int MaximumDefense { get; set; }
	}
}
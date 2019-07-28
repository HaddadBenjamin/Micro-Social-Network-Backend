namespace DiabloII.Items.Reader
{
    public class ItemProperty
    {
        // Maybe name should be an enum to be faster to query
        public string Name { get; set; }
        public int Par { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public bool IsPercent { get; set; }
    }
}
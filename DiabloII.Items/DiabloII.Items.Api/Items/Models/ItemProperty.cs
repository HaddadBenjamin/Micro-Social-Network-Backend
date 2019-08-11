using System;

namespace DiabloII.Items.Api.Items.Responses
{
    public class ItemProperty
    {
		public Guid Id { get; set; }
		public string FormattedName { get; set; }
		public string Name { get; set; }
		public double Par { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public bool IsPercent { get; set; }
		public string FirstChararacter { get; set; }
	}
}
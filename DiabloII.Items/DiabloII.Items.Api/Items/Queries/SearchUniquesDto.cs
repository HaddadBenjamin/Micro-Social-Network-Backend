using System.Collections.Generic;

namespace DiabloII.Items.Api.Items.Queries
{
    public class SearchUniquesDto
    {
        public string Name { get; set; }
        public int LevelRequired { get; set; }
        public string Quality { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> PropertyNames { get; set; }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DiabloII.Items.Api.Items.Responses
{
    public class Item
    {
        public string Name { get; set; }
        public int LevelRequired { get; set; }
        public string Quality { get; set; }
        [JsonProperty("TypeValue")]
        public string Type { get; set; }
        [JsonProperty("SubCategoryValue")]
        public string SubCategory { get; set; }
        [JsonProperty("CategoryValue")]
        public string Category { get; set; }
        public IEnumerable<ItemProperty> Properties { get; set; }
    }
}
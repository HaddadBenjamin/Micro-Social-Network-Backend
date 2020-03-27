using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DiabloII.Items.Api.DbContext.Items;
using DiabloII.Items.Api.DbContext.Items.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DiabloII.Items.Api.Queries.Items
{
    public class SearchUniquesDto
    {
        public string Name { get; set; }
        
        [EnumDataType(typeof(ItemQuality))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemQuality? Quality { get; set; }

        [EnumDataType(typeof(ItemCategory))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemCategory? Category { get; set; }

		/// <summary>
		/// Filter items by it's sub category. ("A", "B", "C"), I'll have to split this.
		/// </summary>
		public string SubCategories { get; set; }
      
        public int? LevelRequired { get; set; }


        public int? MinimumLevel { get; set; }
        public int? MaximumLevel { get; set; }

        public IEnumerable<string> PropertyNames { get; set; }
    }
}
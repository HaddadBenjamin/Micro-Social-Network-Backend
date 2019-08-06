using DiabloII.Items.Api.Items.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiabloII.Items.Api.Items.Queries
{
    // Enum category / sub category
    // revoir les filtres
    public class SearchUniquesDto
    {
        /// <summary>
        /// Filter items by it's name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Filter items by it's quality.
        /// </summary>
        [EnumDataType(typeof(ItemQuality))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemQuality? Quality { get; set; }

        /// <summary>
        /// Filter items by it's category.
        /// </summary>
        [EnumDataType(typeof(ItemCategory))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemCategory? Category { get; set; }

		/// <summary>
		/// Filter items by it's sub category. ("A", "B", "C"), I'll have to split this.
		/// </summary>
		public string SubCategories { get; set; }
      
        /// <summary>
        /// Filter items by it's required level.
        /// </summary>
        public int? LevelRequired { get; set; }


        /// <summary>
        /// Filter items by it's  level.
        /// </summary>
        public int? MinimumLevel { get; set; }
        public int? MaximumLevel { get; set; }

        /// <summary>
        /// Filter items by it's different properties.
        /// </summary>
        public IEnumerable<string> PropertyNames { get; set; }
    }
}
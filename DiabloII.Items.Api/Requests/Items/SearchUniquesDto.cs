using System.ComponentModel.DataAnnotations;
using DiabloII.Items.Api.DbContext.Items.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DiabloII.Items.Api.Requests.Items
{
    public class SearchUniquesDto
    {
        [EnumDataType(typeof(ItemQuality))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemQuality? Quality { get; set; }

		/// <summary>
		/// Filter items by it's sub category. ("A", "B", "C"), I'll have to split this.
		/// </summary>
		public string SubCategories { get; set; }

        public int? MinimumLevel { get; set; }
       
        public int? MaximumLevel { get; set; }
    }
}
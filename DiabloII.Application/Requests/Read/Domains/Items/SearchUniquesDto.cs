﻿using System.ComponentModel.DataAnnotations;
using DiabloII.Domain.Models.Items;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DiabloII.Application.Requests.Read.Domains.Items
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
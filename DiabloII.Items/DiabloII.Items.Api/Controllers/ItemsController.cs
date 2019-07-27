using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DiabloII.Items.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ItemsController : Controller
    {
        // GET api/v1/searchuniques
        [Route("searchuniques")]
        [HttpGet]
        public IEnumerable<Item> SearchUniques()
        {
            var uniquesPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.json");
            var uniquesAsJson = System.IO.File.ReadAllText(uniquesPath);
            var uniques = JsonConvert.DeserializeObject<List<Item>>(uniquesAsJson);

            return uniques;
        }

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
        // Todo :
        // - Swagger (swashbusckle)
        // - SearchDto
        // - SearchResponseDto
        // - watcher
    }
}

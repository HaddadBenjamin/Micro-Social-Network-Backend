using System.Collections.Generic;
using DiabloII.Domain.Models.Items;

namespace DiabloII.Domain.Queries.Items
{
    public class SearchUniquesQuery
    {
        public ItemQuality? Quality { get; set; }
     
        public IEnumerable<string> SubCategories { get; set; }

        public int? MinimumLevel { get; set; }
       
        public int? MaximumLevel { get; set; }
    }
}
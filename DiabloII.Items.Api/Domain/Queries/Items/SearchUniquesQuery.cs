using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.Application.Requests.Items;
using DiabloII.Items.Api.Domain.Models.Items;

namespace DiabloII.Items.Api.Domain.Queries.Items
{
    public class SearchUniquesQuery
    {
        public SearchUniquesQuery(SearchUniquesDto dto)
        {
            Quality = dto.Quality;
            MinimumLevel = dto.MinimumLevel;
            MaximumLevel = dto.MaximumLevel;
            SubCategories = dto.SubCategories?.Split(", ")
                .Select(_ => _
                    .Replace("Two_Handed_Sword", "Two-Handed Sword")
                    .Replace("Wirt_s_Leg", "Wirt's Leg")
                    .Replace("Poorman_s_Head", "Poorman`s Head")
                    .Replace("Hunter_s_Bow", "Hunter’s Bow")
                    .Replace("Chu_Ko_Nu", "Chu-Ko-Nu")
                    .Replace("Bec_De_Corbin", "Bec-De-Corbin")
                    .Replace("Silver_Edged_Axe", "Silver-Edged Axe")
                    .Replace("_", " "));
        }

        public ItemQuality? Quality { get; set; }
     
        public IEnumerable<string> SubCategories { get; set; }

        public int? MinimumLevel { get; set; }
       
        public int? MaximumLevel { get; set; }
    }
}
using DiabloII.Items.Api.Items.Queries;
using DiabloII.Items.Api.Items.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiabloII.Items.Api.Items.Services
{
    public class ItemsService : IItemsService
    {
        public IEnumerable<Item> GetAllUniques()
        {
            var uniquesPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.json");
            var uniquesAsJson = File.ReadAllText(uniquesPath);
            var uniques = JsonConvert.DeserializeObject<List<Item>>(uniquesAsJson);

            return uniques.Where(unique => unique != null);
        }

        public IEnumerable<Item> SearchUniques(SearchUniquesDto dto)
        {
            var uniques = GetAllUniques().ToList();

            if (dto is null)
                return uniques;

            return uniques
                .Where(unique => string.IsNullOrEmpty(dto.Name) ? true : unique.Name.Contains(dto.Name))
                .Where(unique => dto.LevelRequired == 0 ? true : unique.LevelRequired == dto.LevelRequired)
                .Where(unique => dto.Quality == ItemQuality.UNSET ? true : unique.Quality.Contains(dto.Quality.ToString()))
                .Where(unique => dto.Category == ItemCategory.UNSET ? true : unique.Category.Contains(dto.Category.ToString()))
                .Where(unique => dto.SubCategory == ItemSubCategory.UNSET ? true : unique.SubCategory.Contains(dto.SubCategory.ToString()))
                .Where(unique => string.IsNullOrEmpty(dto.Type) ? true : unique.Type.Contains(dto.Type))
                .Where(unique => dto.PropertyNames is null ||!dto.PropertyNames.Any() ? true : unique.Properties.Any(property => dto.PropertyNames.Contains(property.Name)));
        }
    }
}
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

            return uniques;
        }

        public IEnumerable<Item> SearchUniques(SearchUniquesDto dto)
        {
            var uniques = GetAllUniques();

            if (dto is null)
                return uniques;

            return uniques
                .Where(unique => string.IsNullOrEmpty(dto.Name) ? true : unique.Name.Contains(dto.Name))
                .Where(unique => dto.LevelRequired == 0 ? true : unique.LevelRequired == dto.LevelRequired)
                .Where(unique => string.IsNullOrEmpty(dto.Quality) ? true : unique.Name.Contains(dto.Quality))
                .Where(unique => string.IsNullOrEmpty(dto.Category) ? true : unique.Name.Contains(dto.Category))
                .Where(unique => string.IsNullOrEmpty(dto.SubCategory) ? true : unique.Name.Contains(dto.SubCategory))
                .Where(unique => string.IsNullOrEmpty(dto.Type) ? true : unique.Name.Contains(dto.Type))
                .Where(unique => !dto.PropertyNames.Any() ? true : unique.Properties.Any(property => dto.PropertyNames.Contains(property.Name)));
        }
    }
}
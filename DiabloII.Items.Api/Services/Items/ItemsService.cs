using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiabloII.Items.Api.DbContext.Items;
using DiabloII.Items.Api.Queries.Items;
using Newtonsoft.Json;

namespace DiabloII.Items.Api.Services.Items
{
    public class ItemsService : IItemsService
    {
        public async Task<IEnumerable<Item>> GetAllUniques()
        {
            var uniquesPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.json");
            var uniquesAsJson = await File.ReadAllTextAsync(uniquesPath).ConfigureAwait(false);
            var uniques = JsonConvert.DeserializeObject<List<Item>>(uniquesAsJson);

			return uniques.Where(unique => unique != null);
        }

        public async Task<IEnumerable<Item>> SearchUniques(SearchUniquesDto dto)
        {
            var uniques = (await GetAllUniques().ConfigureAwait(false)).ToList();

            if (dto is null)
                return uniques;

			var x = uniques
				//.Where(unique => string.IsNullOrEmpty(dto.Name) ? true : unique.Name.Contains(dto.Name))
				//.Where(unique => dto.LevelRequired is null ? true : unique.LevelRequired == dto.LevelRequired)
				.Where(unique => dto.MinimumLevel is null ? true : unique.Level >= dto.MinimumLevel)
				.Where(unique => dto.MaximumLevel is null ? true : unique.Level <= dto.MaximumLevel)
				//.Where(unique => dto.Quality is null ? true : unique.Quality.Contains(dto.Quality.ToString()))
				//.Where(unique => dto.Category is null || unique.Category is null ? true : unique.Category.Contains(dto.Category.ToString()))
				.Where(unique => (dto.SubCategories is null || string.IsNullOrEmpty(dto.SubCategories) ? true :
					dto.SubCategories.Split(", ")
					.Select (_ => _
						.Replace("Two_Handed_Sword", "Two-Handed Sword")
						.Replace("Wirt_s_Leg", "Wirt's Leg")
						.Replace("Poorman_s_Head", "Poorman`s Head")
						.Replace("Hunter_s_Bow", "Hunter’s Bow")
						.Replace("Chu_Ko_Nu", "Chu-Ko-Nu")
						.Replace("Bec_De_Corbin", "Bec-De-Corbin")
						.Replace("Silver_Edged_Axe", "Silver-Edged Axe")
						.Replace("_", " "))
					.Contains(unique.SubCategory)))
			    // .Where(unique => dto.PropertyNames is null ||!dto.PropertyNames.Any() ? true : unique.Properties.Any(property => dto.PropertyNames.Contains(property.Name)));
                .ToList();
            return x;
        }
    }
}
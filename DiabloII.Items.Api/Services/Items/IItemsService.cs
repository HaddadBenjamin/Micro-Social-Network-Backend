using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Items.Api.DbContext.Items;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Queries.Items;

namespace DiabloII.Items.Api.Services.Items
{
    public interface IItemsService
    {
        Task<IEnumerable<Item>> GetAllUniques();

        Task<IEnumerable<Item>> SearchUniques(SearchUniquesDto dto);
    }
}
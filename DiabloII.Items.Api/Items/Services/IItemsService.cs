using DiabloII.Items.Api.Items.Queries;
using DiabloII.Items.Api.Items.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabloII.Items.Api.Items.Services
{
    public interface IItemsService
    {
        Task<IEnumerable<Item>> GetAllUniques();

        Task<IEnumerable<Item>> SearchUniques(SearchUniquesDto dto);
    }
}
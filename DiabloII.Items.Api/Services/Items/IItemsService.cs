using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Queries;

namespace DiabloII.Items.Api.Services.Items
{
    public interface IItemsService
    {
        void ResetTheItems(IEnumerable<Item> items);

        IReadOnlyCollection<Item> GetAllUniques();

        IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query);
    }
}
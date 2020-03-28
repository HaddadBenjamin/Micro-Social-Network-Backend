using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Queries;

namespace DiabloII.Items.Api.Services.Items
{
    public interface IItemsService
    {
        void ResetTheItems(IList<Item> items);

        IEnumerable<Item> GetAllUniques();

        IEnumerable<Item> SearchUniques(SearchUniquesQuery query);
    }
}
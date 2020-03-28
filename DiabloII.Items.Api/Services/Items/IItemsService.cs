using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Queries;

namespace DiabloII.Items.Api.Services.Items
{
    public interface IItemsService
    {
        void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties);

        IReadOnlyCollection<Item> GetAllUniques();

        IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query);
    }
}
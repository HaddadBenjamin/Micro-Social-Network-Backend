using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Queries.Items;

namespace DiabloII.Items.Api.Repositories.Items
{
    public interface IItemRepository
    {
        #region Read
        IReadOnlyCollection<Item> GetAllUniques();

        IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query);
        #endregion

        #region Write
        void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties);
        #endregion
    }
}
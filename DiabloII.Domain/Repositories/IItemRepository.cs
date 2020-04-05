using System.Collections.Generic;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Items;

namespace DiabloII.Domain.Repositories
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
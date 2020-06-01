using System.Collections.Generic;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Domains.Items;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories.Domains
{
    public interface IItemRepository :
        IRepositoryGetAll<Item>,
        IRepositorySearch<SearchUniquesQuery, Item>
    {
        #region Write
        void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties);
        #endregion
    }
}
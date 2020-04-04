using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.Items;
using DiabloII.Items.Api.Infrastructure.Queries.Items;

namespace DiabloII.Items.Api.Infrastructure.Repositories.Items
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
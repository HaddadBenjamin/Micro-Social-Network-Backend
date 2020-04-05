using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.Items;
using DiabloII.Items.Api.Domain.Queries.Items;

namespace DiabloII.Items.Api.Domain.Services
{
    public interface IItemsService
    {
        void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties);

        IReadOnlyCollection<Item> GetAllUniques();

        IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query);
    }
}
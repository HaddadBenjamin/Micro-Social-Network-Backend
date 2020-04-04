using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.Items;
using DiabloII.Items.Api.Infrastructure.Queries.Items;

namespace DiabloII.Items.Api.Infrastructure.Services.Items
{
    public interface IItemsService
    {
        void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties);

        IReadOnlyCollection<Item> GetAllUniques();

        IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query);
    }
}
using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.Items;
using DiabloII.Items.Api.Domain.Queries.Items;

namespace DiabloII.Items.Api.Domain.Readers
{
    public interface IItemReader
    {
        void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties);

        IReadOnlyCollection<Item> GetAllUniques();

        IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query);
    }
}
using System.Collections.Generic;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Items;

namespace DiabloII.Domain.Readers
{
    public interface IItemReader
    {
        void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties);

        IReadOnlyCollection<Item> GetAllUniques();

        IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query);
    }
}
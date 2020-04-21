using System.Collections.Generic;
using DiabloII.Domain.Models.Items;

namespace DiabloII.Domain.Handlers
{
    public interface IItemCommandHandler
    {
        void Reset(IList<Item> items, IList<ItemProperty> itemProperties);
    }
}
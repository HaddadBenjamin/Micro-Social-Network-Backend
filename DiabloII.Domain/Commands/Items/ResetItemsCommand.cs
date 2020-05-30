using System.Collections.Generic;
using DiabloII.Domain.Models.Items;
using MediatR;

namespace DiabloII.Domain.Commands.Items
{
    public class ResetItemsCommand : IRequest
    {
        public List<Item> Items { get; set; }

        public List<ItemProperty> ItemProperties { get; set; }
    }
}

using DiabloII.Items.Api.Items.Queries;
using DiabloII.Items.Api.Items.Responses;
using System.Collections.Generic;

namespace DiabloII.Items.Api.Items.Services
{
    public interface IItemsService
    {
        IEnumerable<Item> GetAllUniques();

        IEnumerable<Item> SearchUniques(SearchUniquesDto dto);
    }
}
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Items;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Items
{
    public interface IItemsApi :
        IGetAllApi<ItemDto>,
        ISearchApi<SearchUniquesDto, ItemDto>
    {
    }
}
using DiabloII.Application.Requests.Read.Domains.Items;
using DiabloII.Application.Responses.Read.Domains.Items;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Items
{
    public interface IItemsApi :
        IApiGetAll<ItemDto>,
        IApiSearch<SearchUniquesDto, ItemDto>
    {
    }
}
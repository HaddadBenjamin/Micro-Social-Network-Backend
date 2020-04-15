using System.Threading.Tasks;
using DiabloII.Application.Requests.Items;

namespace DiabloII.Application.Tests.Apis.Items
{
    public interface IItemsApi
    {
        Task GetAll();

        Task Search(SearchUniquesDto dto);
    }
}
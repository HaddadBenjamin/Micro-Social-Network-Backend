using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiGetAllWithHals<ResponsesDto>
    {
        Task<ResponsesDto> GetAllWithHals();
    }
}
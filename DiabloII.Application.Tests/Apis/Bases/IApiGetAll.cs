using System.Threading.Tasks;
using DiabloII.Application.Responses;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiGetAll<ResponseDto>
    {
        Task<ApiResponses<ResponseDto>> GetAll();
    }
}
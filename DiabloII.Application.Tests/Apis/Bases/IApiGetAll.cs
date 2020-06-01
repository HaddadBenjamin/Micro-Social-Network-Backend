using System.Threading.Tasks;
using DiabloII.Application.Responses.Read.Bases;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiGetAll<ResponseDto>
    {
        Task<ApiResponses<ResponseDto>> GetAll();
    }
}
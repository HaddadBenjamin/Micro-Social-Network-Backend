using System.Threading.Tasks;
using DiabloII.Application.Responses;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiSearch<RequestDto, ResponseDto>
    {
        Task<ApiResponses<ResponseDto>> Search(RequestDto dto);
    }
}
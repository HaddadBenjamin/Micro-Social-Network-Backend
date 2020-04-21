using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IDeleteApi<DeleteDto, ResponseDto>
    {
        Task<ResponseDto> Delete(DeleteDto dto);
    }
}
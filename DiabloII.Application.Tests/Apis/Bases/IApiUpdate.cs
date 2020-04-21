using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiUpdate<UpdateDto, ResponseDto>
    {
        Task<ResponseDto> Update(UpdateDto dto);
    }
}
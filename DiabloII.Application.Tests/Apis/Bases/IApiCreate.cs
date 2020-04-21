using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiCreate<CreateDto, ResponseDto>
    {
        Task<ResponseDto> Create(CreateDto dto);
    }
}
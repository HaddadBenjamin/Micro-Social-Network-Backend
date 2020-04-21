using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface ICreateApi<CreateDto, ResponseDto>
    {
        Task<ResponseDto> Create(CreateDto dto);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface ISearchApi<RequestDto, ResponseDto>
    {
        Task<IReadOnlyCollection<ResponseDto>> Search(RequestDto dto);
    }
}
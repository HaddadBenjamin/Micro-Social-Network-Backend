using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IGetAllApi<ResponseDto>
    {
        Task<IReadOnlyCollection<ResponseDto>> GetAll();
    }
}
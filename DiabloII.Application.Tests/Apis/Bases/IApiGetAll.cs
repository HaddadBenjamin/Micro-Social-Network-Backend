using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiGetAll<ResponseDto>
    {
        Task<IReadOnlyCollection<ResponseDto>> GetAll();
    }
}
using System.Collections.Generic;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals.Bases
{
    public interface IHalService<Dto>
    {
        HALResponse AddLinks(Dto dto);

        HALResponse AddLinks(IReadOnlyCollection<HALResponse> halResponses);
    }
}
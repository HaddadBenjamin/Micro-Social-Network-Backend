using System.Collections.Generic;
using Halcyon.HAL;

namespace DiabloII.Application.Hals.Bases
{
    public interface IHalDecorator<Dto>
    {
        HALResponse AddLinks(Dto dto);

        HALResponse AddLinks(IReadOnlyCollection<HALResponse> halResponses);
    }
}
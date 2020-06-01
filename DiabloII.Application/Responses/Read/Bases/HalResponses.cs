using System.Collections.Generic;
using Halcyon.HAL;

namespace DiabloII.Application.Responses.Read.Bases
{
    public class HalResponses
    {
        public IReadOnlyCollection<HALResponse> Elements { get; set; }
    }
}
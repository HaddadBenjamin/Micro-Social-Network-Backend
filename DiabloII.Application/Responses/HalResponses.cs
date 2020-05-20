using System.Collections.Generic;
using Halcyon.HAL;

namespace DiabloII.Application.Responses
{
    public class HalResponses
    {
        public IReadOnlyCollection<HALResponse> Elements { get; set; }
    }
}
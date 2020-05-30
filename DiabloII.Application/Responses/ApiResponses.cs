using System.Collections.Generic;

namespace DiabloII.Application.Responses
{
    public class ApiResponses<Element>
    {
        public IReadOnlyCollection<Element> Elements { get; set; }
    }
}
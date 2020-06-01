using System.Collections.Generic;

namespace DiabloII.Application.Responses.Read.Bases
{
    public class ApiResponses<Element>
    {
        public IReadOnlyCollection<Element> Elements { get; set; }
    }
}
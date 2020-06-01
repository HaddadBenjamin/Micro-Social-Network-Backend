using System;
using DiabloII.Application.Requests.Read.Bases;

namespace DiabloII.Application.Requests.Read.Domains.Suggestions
{
    public class GetASuggestionDto : IGetDto
    {
        public Guid Id { get; set; }
    }
}

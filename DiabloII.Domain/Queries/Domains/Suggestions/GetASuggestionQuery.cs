using System;
using DiabloII.Domain.Queries.Bases;

namespace DiabloII.Domain.Queries.Domains.Suggestions
{
    public class GetASuggestionQuery : IGetQuery
    {
        public Guid Id { get; set; }
    }
}

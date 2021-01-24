using System;
using DiabloII.Domain.Queries.Bases;

namespace DiabloII.Domain.Queries.Domains.Notifications
{
    public class GetSuggestionQuery : IGetQuery
    {
        public Guid Id { get; set; }
    }
}
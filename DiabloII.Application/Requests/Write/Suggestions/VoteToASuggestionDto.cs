using System;

namespace DiabloII.Application.Requests.Write.Suggestions
{
    public class VoteToASuggestionDto
    {
        public Guid SuggestionId { get; set; }

        public bool IsPositive { get; set; }

        public string UserId { get; set; }
    }
}
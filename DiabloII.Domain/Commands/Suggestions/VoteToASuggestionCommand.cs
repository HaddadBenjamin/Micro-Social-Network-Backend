using System;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class VoteToASuggestionCommand
    {
        public Guid SuggestionId { get; set; }

        public bool IsPositive { get; set; }

        public string UserId { get; set; }
    }
}

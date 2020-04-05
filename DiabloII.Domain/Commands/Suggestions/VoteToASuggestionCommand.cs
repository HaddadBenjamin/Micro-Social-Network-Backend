using System;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class VoteToASuggestionCommand
    {
        public Guid SuggestionId { get; set; }

        public bool IsPositive { get; set; }

        public string Ip { get; set; }
    }
}

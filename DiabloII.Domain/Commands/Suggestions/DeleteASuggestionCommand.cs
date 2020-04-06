using System;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class DeleteASuggestionCommand
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
    }
}

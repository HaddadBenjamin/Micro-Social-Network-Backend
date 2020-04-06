using System;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class CommentASuggestionCommand
    {
        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }
    }
}

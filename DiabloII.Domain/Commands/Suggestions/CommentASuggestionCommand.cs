using System;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class CommentASuggestionCommand
    {
        public Guid SuggestionId { get; set; }

        public string Ip { get; set; }

        public string Comment { get; set; }
    }
}

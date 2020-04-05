using System;

namespace DiabloII.Items.Api.Domain.Commands.Suggestions
{
    public class CommentASuggestionCommand
    {
        public Guid SuggestionId { get; set; }

        public string Ip { get; set; }

        public string Comment { get; set; }
    }
}

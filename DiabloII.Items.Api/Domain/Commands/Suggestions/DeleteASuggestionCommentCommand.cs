using System;

namespace DiabloII.Items.Api.Domain.Commands.Suggestions
{
    public class DeleteASuggestionCommentCommand
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public string Ip { get; set; }
    }
}

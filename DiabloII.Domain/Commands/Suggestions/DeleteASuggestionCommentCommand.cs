using System;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class DeleteASuggestionCommentCommand
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }
    }
}

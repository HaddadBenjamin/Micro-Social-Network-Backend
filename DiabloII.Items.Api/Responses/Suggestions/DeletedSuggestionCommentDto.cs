using System;

namespace DiabloII.Items.Api.Responses.Suggestions
{
    public class DeletedSuggestionCommentDto
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }
    }
}
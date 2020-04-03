using System;

namespace DiabloII.Items.Api.Requests.Suggestions
{
    public class DeleteASuggestionCommentDto
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public string Ip { get; set; }
    }
}
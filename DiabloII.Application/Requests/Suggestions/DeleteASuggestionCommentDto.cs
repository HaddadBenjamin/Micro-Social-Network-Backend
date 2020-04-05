using System;

namespace DiabloII.Application.Requests.Suggestions
{
    public class DeleteASuggestionCommentDto
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public string Ip { get; set; }
    }
}
using System;

namespace DiabloII.Application.Requests.Write.Suggestions
{
    public class CommentASuggestionDto
    {
        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }

    }
}
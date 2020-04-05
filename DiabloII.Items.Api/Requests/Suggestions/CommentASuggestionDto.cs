using System;

namespace DiabloII.Application.Requests.Suggestions
{
    public class CommentASuggestionDto
    {
        public Guid SuggestionId { get; set; }

        public string Ip { get; set; }
        
        public string Comment { get; set; }

    }
}
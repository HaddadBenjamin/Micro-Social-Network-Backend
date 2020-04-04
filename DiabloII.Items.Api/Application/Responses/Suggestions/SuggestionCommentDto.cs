using System;

namespace DiabloII.Items.Api.Application.Responses.Suggestions
{
    public class SuggestionCommentDto
    {
        public Guid Id { get; set; }

        public string Ip { get; set; }
        
        public string Comment { get; set; }
    }
}
using System;

namespace DiabloII.Application.Responses.Read.Suggestions
{
    public class SuggestionCommentDto
    {
        public Guid Id { get; set; }

        public string CreatedBy { get; set; }

        public string Comment { get; set; }
    }
}
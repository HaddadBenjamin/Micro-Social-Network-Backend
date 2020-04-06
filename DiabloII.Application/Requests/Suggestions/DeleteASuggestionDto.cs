using System;

namespace DiabloII.Application.Requests.Suggestions
{
    public class DeleteASuggestionDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
    }
}
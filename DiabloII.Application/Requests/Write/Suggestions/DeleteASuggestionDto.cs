using System;

namespace DiabloII.Application.Requests.Write.Suggestions
{
    public class DeleteASuggestionDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
    }
}
using System;

namespace DiabloII.Application.Requests.Suggestions
{
    public class DeleteASuggestionDto
    {
        public Guid Id { get; set; }

        public string Ip { get; set; }
    }
}
using System;

namespace DiabloII.Items.Api.Requests.Suggestions
{
    public class DeleteASuggestionDto
    {
        public Guid Id { get; set; }

        public string Ip { get; set; }
    }
}
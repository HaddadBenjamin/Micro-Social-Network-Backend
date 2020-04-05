using System;

namespace DiabloII.Items.Api.Domain.Commands.Suggestions
{
    public class DeleteASuggestionCommand
    {
        public Guid Id { get; set; }

        public string Ip { get; set; }
    }
}

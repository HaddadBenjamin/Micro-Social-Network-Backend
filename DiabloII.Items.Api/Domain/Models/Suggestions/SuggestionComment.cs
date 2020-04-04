using System;

namespace DiabloII.Items.Api.Domain.Models.Suggestions
{
    public class SuggestionComment
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public virtual Suggestion Suggestion { get; set; }

        public string Ip { get; set; }

        public string Comment { get; set; }
    }
}
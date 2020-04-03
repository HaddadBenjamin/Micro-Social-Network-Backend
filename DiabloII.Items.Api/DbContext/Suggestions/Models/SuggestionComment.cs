using System;

namespace DiabloII.Items.Api.DbContext.Suggestions.Models
{
    public class SuggestionComment
    {
        public Guid Id { get; set; }

        public Suggestion Suggestion { get; set; }

        public string Ip { get; set; }

        public string Comment { get; set; }
    }
}
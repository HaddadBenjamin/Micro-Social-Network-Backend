using System;

namespace DiabloII.Domain.Models.Suggestions
{
    public class SuggestionComment
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public virtual Suggestion Suggestion { get; set; }

        public string CreatedBy { get; set; }

        public string Comment { get; set; }
    }
}
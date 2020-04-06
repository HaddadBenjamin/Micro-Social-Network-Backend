using System;

namespace DiabloII.Domain.Models.Suggestions
{
    public class SuggestionVote
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }
       
        public virtual Suggestion Suggestion { get; set; }

        public bool IsPositive { get; set; }

        public string CreatedBy { get; set; }
    }
}
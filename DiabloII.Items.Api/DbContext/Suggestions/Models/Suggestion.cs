using System;
using System.Collections.Generic;

namespace DiabloII.Items.Api.DbContext.Suggestions.Models
{
    public class Suggestion
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; }

        public string Ip { get; set; }

        public virtual ICollection<SuggestionVote> Votes { get; set; } = new List<SuggestionVote>();
       
        public virtual ICollection<SuggestionComment> Comments { get; set; } = new List<SuggestionComment>();
    }
}

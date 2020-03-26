using System.Collections.Generic;

namespace DiabloII.Items.Api.DbContext.Suggestions
{
    public class Suggestion
    {
        public int Id { get; set; }
        
        public string Content { get; set; }

        public virtual ICollection<SuggestionVote> Votes { get; set; } = new List<SuggestionVote>();
    }
}

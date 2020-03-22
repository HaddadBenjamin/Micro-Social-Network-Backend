using System;

namespace DiabloII.Items.Api.Queries.Suggestions
{
    public class SuggestionVoteDto
    {
        public int SuggestionId { get; set; }
        
        public bool IsPositive { get; set; }
        
        public string Ip { get; set; }
    }
}
using System;

namespace DiabloII.Items.Api.Queries.Suggestions
{
    public class VoteToASuggestionDto
    {
        public int SuggestionId { get; set; }
        
        public bool IsPositive { get; set; }
        
        public string Ip { get; set; }
    }
}
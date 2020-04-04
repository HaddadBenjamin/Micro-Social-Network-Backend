using System;

namespace DiabloII.Items.Api.Requests.Suggestions
{
    public class VoteToASuggestionDto
    {
        public Guid SuggestionId { get; set; }
        
        public bool IsPositive { get; set; }
        
        public string Ip { get; set; }
    }
}
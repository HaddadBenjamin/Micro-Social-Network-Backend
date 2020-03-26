using System;

namespace DiabloII.Items.Api.Responses.Suggestions
{
    public class SuggestionDto
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; }
        
        public int PositiveVoteCount { get; set; }
       
        public int NegativeVoteCount { get; set; }
    }
}
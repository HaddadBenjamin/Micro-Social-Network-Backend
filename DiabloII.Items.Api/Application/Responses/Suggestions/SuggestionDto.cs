using System;
using System.Collections.Generic;

namespace DiabloII.Items.Api.Application.Responses.Suggestions
{
    public class SuggestionDto
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; }

        public string Ip { get; set; }

        public int PositiveVoteCount { get; set; }
       
        public int NegativeVoteCount { get; set; }

        public IReadOnlyCollection<SuggestionVoteDto> Votes { get; set; }

        public IReadOnlyCollection<SuggestionCommentDto> Comments { get; set; }

    }
}
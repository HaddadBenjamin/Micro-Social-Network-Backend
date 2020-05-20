using System;
using System.Collections.Generic;
using Halcyon.HAL;

namespace DiabloII.Application.Responses.Suggestions
{
    public class SuggestionDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public int PositiveVoteCount { get; set; }

        public int NegativeVoteCount { get; set; }

        public IReadOnlyCollection<SuggestionVoteDto> Votes { get; set; }

        public IReadOnlyCollection<SuggestionCommentDto> Comments { get; set; }
    }

    public class SuggestionHalResponse
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public int PositiveVoteCount { get; set; }

        public int NegativeVoteCount { get; set; }

        public IReadOnlyCollection<SuggestionVoteDto> Votes { get; set; }

        public IReadOnlyCollection<HALResponse> Comments { get; set; }
    }
}
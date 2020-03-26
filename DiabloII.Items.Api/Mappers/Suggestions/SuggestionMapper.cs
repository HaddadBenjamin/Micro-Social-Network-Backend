﻿using System.Linq;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;

namespace DiabloII.Items.Api.Mappers.Suggestions
{
    public static class SuggestionMapper
    {
        public static SuggestionDto ToSuggestionDto(Suggestion suggestion) => new SuggestionDto
        {
            Id = suggestion.Id,
            Content = suggestion.Content,
            PositiveVoteCount = suggestion.Votes.Count(vote => vote.IsPositive),
            NegativeVoteCount = suggestion.Votes.Count(vote => !vote.IsPositive),
        };

        public static Suggestion ToSuggestion(CreateASuggestionDto createASuggestionDto) => new Suggestion
        {
            Content = createASuggestionDto.Content,
        };

        public static SuggestionVote ToSuggestionVote(VoteToASuggestionDto voteToASuggestionDto) => new SuggestionVote
        {
            SuggestionId = voteToASuggestionDto.SuggestionId,
            IsPositive = voteToASuggestionDto.IsPositive,
            Ip = voteToASuggestionDto.Ip
        };
    }
}
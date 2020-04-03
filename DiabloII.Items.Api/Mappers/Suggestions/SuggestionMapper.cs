using System;
using System.Linq;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using DiabloII.Items.Api.Requests.Suggestions;
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
            Votes = suggestion.Votes.Select(vote => new SuggestionVoteDto
            {
                Ip = vote.Ip,
                IsPositive = vote.IsPositive
            }).ToList()
        };

        public static Suggestion ToSuggestion(CreateASuggestionDto createASuggestionDto) => new Suggestion
        {
            Id = Guid.NewGuid(),
            Content = createASuggestionDto.Content,
        };

        public static SuggestionVote ToSuggestionVote(VoteToASuggestionDto voteToASuggestionDto) => new SuggestionVote
        {
            Id = Guid.NewGuid(),
            SuggestionId = voteToASuggestionDto.SuggestionId,
            IsPositive = voteToASuggestionDto.IsPositive,
            Ip = voteToASuggestionDto.Ip
        };
    }
}
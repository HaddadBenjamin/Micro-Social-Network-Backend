using System;
using System.Linq;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;

namespace DiabloII.Items.Api.Services.Suggestions
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

        public static Suggestion ToSuggestion(CreateSuggestionDto createSuggestionDto) => new Suggestion
        {
            Id = Guid.NewGuid(),
            Content = createSuggestionDto.Content,
        };

        public static SuggestionVote ToSuggestionVote(SuggestionVoteDto suggestionVoteDto) => new SuggestionVote
        {
            Id = Guid.NewGuid(),
            SuggestionId = suggestionVoteDto.SuggestionId,
            IsPositive = suggestionVoteDto.IsPositive,
            Ip = suggestionVoteDto.Ip
        };
    }
}
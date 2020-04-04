using System.Linq;
using DiabloII.Items.Api.Application.Responses.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Application.Mappers.Suggestions
{
    public static class SuggestionDtoMapper
    {
        public static SuggestionDto ToSuggestionDto(Suggestion suggestion) => new SuggestionDto
        {
            Id = suggestion.Id,
            Ip = suggestion.Ip,
            Content = suggestion.Content,
            PositiveVoteCount = suggestion.Votes.Count(vote => vote.IsPositive),
            NegativeVoteCount = suggestion.Votes.Count(vote => !vote.IsPositive),
            Votes = suggestion.Votes.Select(ToSugugestionVoteDto).ToList(),
            Comments = suggestion.Comments.Select(ToSuggestionCommentDto).ToList()
        };

        private static SuggestionCommentDto ToSuggestionCommentDto(SuggestionComment comment) => new SuggestionCommentDto
        {
            Id = comment.Id,
            Ip = comment.Ip, 
            Comment = comment.Comment
        };

        private static SuggestionVoteDto ToSugugestionVoteDto(SuggestionVote vote) => new SuggestionVoteDto
        {
            Ip = vote.Ip,
            IsPositive = vote.IsPositive
        };
    }
}
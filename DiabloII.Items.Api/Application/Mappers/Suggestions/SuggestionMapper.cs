using System;
using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Application.Mappers.Suggestions
{
    public static class SuggestionMapper
    {
        public static Suggestion ToSuggestion(CreateASuggestionDto createASuggestionDto) => new Suggestion
        {
            Id = Guid.NewGuid(),
            Ip = createASuggestionDto.Ip,
            Content = createASuggestionDto.Content,
        };

        public static SuggestionVote ToSuggestionVote(VoteToASuggestionDto voteToASuggestionDto) => new SuggestionVote
        {
            Id = Guid.NewGuid(),
            IsPositive = voteToASuggestionDto.IsPositive,
            Ip = voteToASuggestionDto.Ip
        };

        public static SuggestionComment ToSuggestionComment(CommentASuggestionDto commentASuggestionDto) => new SuggestionComment
        {
            Id = Guid.NewGuid(),
            Comment = commentASuggestionDto.Comment,
            Ip = commentASuggestionDto.Ip
        };
    }
}
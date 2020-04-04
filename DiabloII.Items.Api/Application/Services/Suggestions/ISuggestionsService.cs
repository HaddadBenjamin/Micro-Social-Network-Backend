using System;
using System.Collections.Generic;
using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Application.Services.Suggestions
{
    public interface ISuggestionsService
    {
        Suggestion Create(CreateASuggestionDto createASugestion);

        IReadOnlyCollection<Suggestion> GetAll();
     
        Suggestion Vote(VoteToASuggestionDto voteToASuggestion);

        Suggestion Comment(CommentASuggestionDto commentASuggestion);

        Guid Delete(DeleteASuggestionDto deleteASuggestion);

        Suggestion DeleteAComment(DeleteASuggestionCommentDto deleteASuggestionComment);
    }
}
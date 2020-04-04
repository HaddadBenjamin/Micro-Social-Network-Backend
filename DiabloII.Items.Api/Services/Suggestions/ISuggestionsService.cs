using System;
using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Services.Suggestions
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
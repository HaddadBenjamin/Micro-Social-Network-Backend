using System;
using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Application.Services.Suggestions
{
    public interface ISuggestionsService
    {
        Suggestion Create(CreateASuggestionCommand createASugestion);

        IReadOnlyCollection<Suggestion> GetAll();
     
        Suggestion Vote(VoteToASuggestionCommand voteToASuggestion);

        Suggestion Comment(CommentASuggestionCommand commentASuggestion);

        Guid Delete(DeleteASuggestionCommand deleteASuggestion);

        Suggestion DeleteAComment(DeleteASuggestionCommentCommand deleteASuggestionComment);
    }
}
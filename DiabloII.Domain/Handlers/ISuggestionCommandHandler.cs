using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Handlers
{
    public interface ISuggestionCommandHandler
    {
        Suggestion Create(CreateASuggestionCommand createASugestion);

        Suggestion Vote(VoteToASuggestionCommand voteToASuggestion);

        Suggestion Comment(CommentASuggestionCommand commentASuggestion);

        Guid Delete(DeleteASuggestionCommand deleteASuggestion);

        Suggestion DeleteAComment(DeleteASuggestionCommentCommand deleteASuggestionComment);
    }
}
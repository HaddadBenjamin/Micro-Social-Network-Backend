using System;
using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Domain.Handlers
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
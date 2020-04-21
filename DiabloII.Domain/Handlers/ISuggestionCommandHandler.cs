using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Handlers.Bases;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Handlers
{
    public interface ISuggestionCommandHandler :
        ICreateCommandHandler<CreateASuggestionCommand, Suggestion>,
        ICreateCommandHandler<VoteToASuggestionCommand, Suggestion>,
        ICreateCommandHandler<CommentASuggestionCommand, Suggestion>,
        IDeleteCommandHandler<DeleteASuggestionCommand, Guid>,
        IDeleteCommandHandler<DeleteASuggestionCommentCommand, Suggestion>
    {
    }
}
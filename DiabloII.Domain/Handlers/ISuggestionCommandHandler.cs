using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Handlers.Bases;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Handlers
{
    public interface ISuggestionCommandHandler :
        ICommandHandlerCreate<CreateASuggestionCommand, Suggestion>,
        ICommandHandlerCreate<VoteToASuggestionCommand, Suggestion>,
        ICommandHandlerCreate<CommentASuggestionCommand, Suggestion>,
        ICommandHandlerDelete<DeleteASuggestionCommand, Guid>,
        ICommandHandlerDelete<DeleteASuggestionCommentCommand, Suggestion>
    {
    }
}
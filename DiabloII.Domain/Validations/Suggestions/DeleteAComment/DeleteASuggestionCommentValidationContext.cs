using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidationContext
    {
        public DeleteASuggestionCommentCommand Command { get; set; }

        public ISuggestionRepository Repository { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; set; }

        public DeleteASuggestionCommentValidationContext(DeleteASuggestionCommentCommand command, ISuggestionRepository repository)
        {
            Command = command;
            Repository = repository;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, Command.SuggestionId);
            DbContextValidationContext.CommentId = Command.Id;
        }
    }
}
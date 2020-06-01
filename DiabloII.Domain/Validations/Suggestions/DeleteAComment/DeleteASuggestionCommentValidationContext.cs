using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Domain.Validations.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidationContext
    {
        public DeleteASuggestionCommentCommand Command { get; set; }

        public ISuggestionRepository Repository { get; }

        public CommonSuggestionRepositoryValidationContext RepositoryValidationContext { get; set; }

        public DeleteASuggestionCommentValidationContext(DeleteASuggestionCommentCommand command, ISuggestionRepository repository)
        {
            Command = command;
            Repository = repository;
            RepositoryValidationContext = new CommonSuggestionRepositoryValidationContext(repository, Command.SuggestionId);
            RepositoryValidationContext.CommentId = Command.Id;
        }
    }
}
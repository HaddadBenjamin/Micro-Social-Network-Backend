using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Domain.Validations.Suggestions.Comment
{
    public class CommentASuggestionValidationContext
    {
        public CommentASuggestionCommand Command { get; set; }

        public CommonSuggestionRepositoryValidationContext RepositoryValidationContext { get; }

        public CommentASuggestionValidationContext(CommentASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            RepositoryValidationContext = new CommonSuggestionRepositoryValidationContext(repository, Command.SuggestionId);
        }
    }
}
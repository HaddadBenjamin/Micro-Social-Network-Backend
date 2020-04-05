using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Suggestions.Comment
{
    public class CommentASuggestionValidationContext
    {
        public CommentASuggestionCommand Command { get; set; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public CommentASuggestionValidationContext(CommentASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, Command.SuggestionId);
        }
    }
}
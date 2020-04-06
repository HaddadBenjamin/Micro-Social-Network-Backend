using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Suggestions.Delete
{
    public class DeleteASuggestionValidationContext
    {
        public DeleteASuggestionCommand Command { get; set; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; set; }

        public DeleteASuggestionValidationContext(DeleteASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, Command.Id)
            {
                UserId = Command.UserId
            };
        }
    }
}
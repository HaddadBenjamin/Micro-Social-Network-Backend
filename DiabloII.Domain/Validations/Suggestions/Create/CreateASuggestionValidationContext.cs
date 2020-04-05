using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Suggestions.Create
{
    public class CreateASuggestionValidationContext
    {
        public CreateASuggestionCommand Command { get; set; }
      
        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public CreateASuggestionValidationContext(CreateASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository)
            {
                Content = Command.Content
            };
        }
    }
}
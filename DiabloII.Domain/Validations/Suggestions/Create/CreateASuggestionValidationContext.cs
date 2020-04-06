using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Suggestions.Create
{
    public class CreateASuggestionValidationContext
    {
        public CreateASuggestionCommand Command { get; set; }
      
        public CommonSuggestionRepositoryValidationContext RepositoryValidationContext { get; }

        public CreateASuggestionValidationContext(CreateASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            RepositoryValidationContext = new CommonSuggestionRepositoryValidationContext(repository)
            {
                Content = Command.Content
            };
        }
    }
}
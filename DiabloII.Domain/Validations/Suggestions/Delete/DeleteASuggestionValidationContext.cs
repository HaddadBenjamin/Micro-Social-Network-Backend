using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Domain.Validations.Suggestions.Delete
{
    public class DeleteASuggestionValidationContext
    {
        public DeleteASuggestionCommand Command { get; set; }

        public CommonSuggestionRepositoryValidationContext RepositoryValidationContext { get; set; }

        public DeleteASuggestionValidationContext(DeleteASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            RepositoryValidationContext = new CommonSuggestionRepositoryValidationContext(repository, Command.Id)
            {
                UserId = Command.UserId
            };
        }
    }
}
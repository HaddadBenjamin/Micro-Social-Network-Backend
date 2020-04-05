using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.Delete
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
                Ip = Command.Ip
            };
        }
    }
}
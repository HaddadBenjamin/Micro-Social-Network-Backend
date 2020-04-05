using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.Vote
{
    public class VoteToASuggestionValidationContext
    {
        public VoteToASuggestionCommand Command { get; set; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public VoteToASuggestionValidationContext(VoteToASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, Command.SuggestionId);
        }
    }
}
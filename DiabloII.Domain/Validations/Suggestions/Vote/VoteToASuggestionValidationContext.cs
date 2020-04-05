using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Suggestions.Vote
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
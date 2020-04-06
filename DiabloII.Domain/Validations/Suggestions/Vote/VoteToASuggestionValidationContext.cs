using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Repositories;

namespace DiabloII.Domain.Validations.Suggestions.Vote
{
    public class VoteToASuggestionValidationContext
    {
        public VoteToASuggestionCommand Command { get; set; }

        public CommonSuggestionRepositoryValidationContext RepositoryValidationContext { get; }

        public VoteToASuggestionValidationContext(VoteToASuggestionCommand command, ISuggestionRepository repository)
        {
            Command = command;
            RepositoryValidationContext = new CommonSuggestionRepositoryValidationContext(repository, Command.SuggestionId);
        }
    }
}
using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Vallidations.Suggestions.Vote
{
    public class VoteToASuggestionValidationContext
    {
        public VoteToASuggestionDto Dto { get; set; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public VoteToASuggestionValidationContext(VoteToASuggestionDto dto, ISuggestionRepository repository)
        {
            Dto = dto;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, dto.SuggestionId);
        }
    }
}
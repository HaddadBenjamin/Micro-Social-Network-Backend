using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.Vote
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
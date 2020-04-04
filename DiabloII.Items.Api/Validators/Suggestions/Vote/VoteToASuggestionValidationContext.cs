using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Vote
{
    public class VoteToASuggestionValidationContext
    {
        public VoteToASuggestionDto Dto { get; set; }
       
        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public VoteToASuggestionValidationContext(VoteToASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidationContext = new SuggestionDbContextValidationContext(dbContext, dto.SuggestionId);
        }
    }
}
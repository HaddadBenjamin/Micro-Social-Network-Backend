using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Vote
{
    public class VoteToASuggestionValidatorContext
    {
        public VoteToASuggestionDto Dto { get; set; }
       
        public ApplicationDbContext DbContext { get; }

        public VoteToASuggestionValidatorContext(VoteToASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
        }
    }
}
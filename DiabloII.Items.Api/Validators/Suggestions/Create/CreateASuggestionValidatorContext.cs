using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Queries.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public class CreateASuggestionValidatorContext
    {
        public CreateASuggestionDto Dto { get; set; }
      
        public ApplicationDbContext DbContext { get; }

        public CreateASuggestionValidatorContext(CreateASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
        }
    }
}
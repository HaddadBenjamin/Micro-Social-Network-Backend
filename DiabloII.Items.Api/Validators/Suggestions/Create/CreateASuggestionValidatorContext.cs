using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public class CreateASuggestionValidatorContext
    {
        public CreateASuggestionDto Dto { get; set; }
      
        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidatorContext DbContextValidatorContext { get; }

        public CreateASuggestionValidatorContext(CreateASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidatorContext = new SuggestionDbContextValidatorContext(dbContext)
            {
                Content = dto.Content
            };
        }
    }
}
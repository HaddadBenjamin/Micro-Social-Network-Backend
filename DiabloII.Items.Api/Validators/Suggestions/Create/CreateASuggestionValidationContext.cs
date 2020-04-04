using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public class CreateASuggestionValidationContext
    {
        public CreateASuggestionDto Dto { get; set; }
      
        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public CreateASuggestionValidationContext(CreateASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidationContext = new SuggestionDbContextValidationContext(dbContext)
            {
                Content = dto.Content
            };
        }
    }
}
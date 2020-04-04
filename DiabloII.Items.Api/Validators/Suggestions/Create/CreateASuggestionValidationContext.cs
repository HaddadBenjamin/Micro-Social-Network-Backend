using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Services.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public class CreateASuggestionValidationContext
    {
        public CreateASuggestionDto Dto { get; set; }
      
        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public CreateASuggestionValidationContext(CreateASuggestionDto dto, ISuggestionRepository repository)
        {
            Dto = dto;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository)
            {
                Content = dto.Content
            };
        }
    }
}
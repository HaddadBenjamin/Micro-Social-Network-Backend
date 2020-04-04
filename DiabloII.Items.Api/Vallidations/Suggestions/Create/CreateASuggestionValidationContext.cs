using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Vallidations.Suggestions.Create
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
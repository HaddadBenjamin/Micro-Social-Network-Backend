using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.Create
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
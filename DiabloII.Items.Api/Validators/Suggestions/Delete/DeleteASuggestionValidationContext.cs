using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Delete
{
    public class DeleteASuggestionValidationContext
    {
        public DeleteASuggestionDto Dto { get; set; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; set; }

        public DeleteASuggestionValidationContext(DeleteASuggestionDto dto, ISuggestionRepository repository)
        {
            Dto = dto;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, dto.Id)
            {
                Ip = dto.Ip
            };
        }
    }
}
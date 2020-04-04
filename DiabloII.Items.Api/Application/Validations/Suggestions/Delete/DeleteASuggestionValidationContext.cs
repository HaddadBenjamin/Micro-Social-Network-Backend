using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.Delete
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
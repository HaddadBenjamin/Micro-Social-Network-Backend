using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Infrastructure.Validations.Suggestions.Comment
{
    public class CommentASuggestionValidationContext
    {
        public CommentASuggestionDto Dto { get; set; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public CommentASuggestionValidationContext(CommentASuggestionDto dto, ISuggestionRepository repository)
        {
            Dto = dto;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, dto.SuggestionId);
        }
    }
}
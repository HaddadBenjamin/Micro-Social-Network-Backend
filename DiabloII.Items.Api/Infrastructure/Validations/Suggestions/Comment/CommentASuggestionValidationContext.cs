using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validations.Suggestions.Comment
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
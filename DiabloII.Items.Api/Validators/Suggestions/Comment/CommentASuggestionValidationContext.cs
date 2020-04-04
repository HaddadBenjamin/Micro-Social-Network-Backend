using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Services.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Comment
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
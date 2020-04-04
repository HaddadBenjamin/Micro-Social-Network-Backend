using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Comment
{
    public class CommentASuggestionValidationContext
    {
        public CommentASuggestionDto Dto { get; set; }

        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; }

        public CommentASuggestionValidationContext(CommentASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidationContext = new SuggestionDbContextValidationContext(dbContext, dto.SuggestionId);
        }
    }
}
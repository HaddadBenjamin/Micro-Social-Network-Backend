using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Comment
{
    public class CommentASuggestionValidatorContext
    {
        public CommentASuggestionDto Dto { get; set; }

        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidatorContext DbContextValidatorContext { get; }

        public CommentASuggestionValidatorContext(CommentASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidatorContext = new SuggestionDbContextValidatorContext(dbContext, dto.SuggestionId);
        }
    }
}
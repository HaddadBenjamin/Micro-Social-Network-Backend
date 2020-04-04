using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidatorContext
    {
        public DeleteASuggestionCommentDto Dto { get; set; }

        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidatorContext DbContextValidatorContext { get; set; }

        public DeleteASuggestionCommentValidatorContext(DeleteASuggestionCommentDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidatorContext = new SuggestionDbContextValidatorContext(dbContext, dto.Id);
        }
    }
}
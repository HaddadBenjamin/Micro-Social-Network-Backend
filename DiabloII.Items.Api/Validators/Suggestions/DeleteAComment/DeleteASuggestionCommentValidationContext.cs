using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidationContext
    {
        public DeleteASuggestionCommentDto Dto { get; set; }

        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; set; }

        public DeleteASuggestionCommentValidationContext(DeleteASuggestionCommentDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidationContext = new SuggestionDbContextValidationContext(dbContext, dto.Id);
        }
    }
}
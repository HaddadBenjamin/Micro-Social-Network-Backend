using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Vallidations.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidationContext
    {
        public DeleteASuggestionCommentDto Dto { get; set; }

        public ISuggestionRepository Repository { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; set; }

        public DeleteASuggestionCommentValidationContext(DeleteASuggestionCommentDto dto, ISuggestionRepository repository)
        {
            Dto = dto;
            Repository = repository;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, dto.SuggestionId);
            DbContextValidationContext.CommentId = Dto.Id;
        }
    }
}
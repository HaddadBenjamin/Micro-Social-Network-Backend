using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.DeleteAComment
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
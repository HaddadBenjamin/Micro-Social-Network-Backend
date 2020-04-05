using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidationContext
    {
        public DeleteASuggestionCommentCommand Command { get; set; }

        public ISuggestionRepository Repository { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; set; }

        public DeleteASuggestionCommentValidationContext(DeleteASuggestionCommentCommand command, ISuggestionRepository repository)
        {
            Command = command;
            Repository = repository;
            DbContextValidationContext = new SuggestionDbContextValidationContext(repository, Command.SuggestionId);
            DbContextValidationContext.CommentId = Command.Id;
        }
    }
}
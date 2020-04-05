using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidator : AbstractValidator<DeleteASuggestionCommentValidationContext>
    {
        public DeleteASuggestionCommentValidator()
        {
            RuleFor(context => context.Command.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidationContext)
                .SuggestionShouldExists()
                .SuggestionCommentShouldExists();
            RuleFor(context => context).SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp();
        }
    }
}
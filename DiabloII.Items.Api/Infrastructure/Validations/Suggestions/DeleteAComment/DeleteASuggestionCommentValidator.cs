using FluentValidation;

namespace DiabloII.Items.Api.Infrastructure.Validations.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidator : AbstractValidator<DeleteASuggestionCommentValidationContext>
    {
        public DeleteASuggestionCommentValidator()
        {
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidationContext)
                .SuggestionShouldExists()
                .SuggestionCommentShouldExists();
            RuleFor(context => context).SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp();
        }
    }
}
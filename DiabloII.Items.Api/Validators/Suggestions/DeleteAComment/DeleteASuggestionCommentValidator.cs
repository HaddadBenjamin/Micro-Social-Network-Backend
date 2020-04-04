using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidator : AbstractValidator<DeleteASuggestionCommentValidatorContext>
    {
        public DeleteASuggestionCommentValidator()
        {
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context).SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp();
        }
    }
}
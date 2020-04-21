using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidator : AbstractValidator<DeleteASuggestionCommentValidationContext>
    {
        public DeleteASuggestionCommentValidator()
        {
            RuleFor(context => context.Command.UserId).ShouldNotBeNullOrEmpty("UserId");

            RuleFor(context => context.RepositoryValidationContext)
                .SuggestionShouldExists()
                .SuggestionCommentShouldExists();

            RuleFor(context => context).ShouldBeOwnerOfTheComment();
        }
    }
}
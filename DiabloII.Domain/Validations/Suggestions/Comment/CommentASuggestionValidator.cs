using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.Comment
{
    public class CommentASuggestionValidator : AbstractValidator<CommentASuggestionValidationContext>
    {
        public CommentASuggestionValidator()
        {
            RuleFor(context => context.Command.UserId).ShouldNotBeNullOrEmpty("UserId");

            RuleFor(context => context.Command.Comment)
                .ShouldNotBeNullOrEmpty("Comment")
                .ShouldBeShorterThan("Comment");

            RuleFor(context => context.RepositoryValidationContext).SuggestionShouldExists();
        }
    }
}
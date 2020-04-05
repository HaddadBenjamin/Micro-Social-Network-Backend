using FluentValidation;

namespace DiabloII.Items.Api.Infrastructure.Validations.Suggestions.Comment
{
    public class CommentASuggestionValidator : AbstractValidator<CommentASuggestionValidationContext>
    {
        public CommentASuggestionValidator()
        {
            RuleFor(context => context.Command.Comment)
                .ShouldNotBeNullOrEmpty("Comment")
                .ShouldBeShorterThan("Comment");
            RuleFor(context => context.Command.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidationContext).SuggestionShouldExists();
        }
    }
}
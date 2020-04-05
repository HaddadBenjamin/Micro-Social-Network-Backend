using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.Delete
{
    public class DeleteASuggestionValidator : AbstractValidator<DeleteASuggestionValidationContext>
    {
        public DeleteASuggestionValidator()
        {
            RuleFor(context => context.Command.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidationContext)
                .SuggestionShouldExists()
                .SuggestionShouldBeRelatedToTheUserIp();
        }
    }
}
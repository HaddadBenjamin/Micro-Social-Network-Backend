using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.Delete
{
    public class DeleteASuggestionValidator : AbstractValidator<DeleteASuggestionValidationContext>
    {
        public DeleteASuggestionValidator()
        {
            RuleFor(context => context.Command.UserId).ShouldNotBeNullOrEmpty("UserId");
            
            RuleFor(context => context.DbContextValidationContext)
                .SuggestionShouldExists()
                .SuggestionShouldBeRelatedToTheUserIp();
        }
    }
}
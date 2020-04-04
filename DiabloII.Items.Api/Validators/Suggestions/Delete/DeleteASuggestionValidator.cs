using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Delete
{
    public class DeleteASuggestionValidator : AbstractValidator<DeleteASuggestionValidatorContext>
    {
        public DeleteASuggestionValidator()
        {
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidatorContext).SuggestionShouldExistsAndBeRelatedToTheUserIp();
        }
    }
}
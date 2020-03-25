using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public class CreateASuggestionValidator : AbstractValidator<CreateASuggestionValidatorContext>
    {
        public CreateASuggestionValidator()
        {
            RuleFor(context => context.Dto.Content).SuggestionContentShouldNotBeNullOrEmpty();
            RuleFor(context => context).SuggestionContentShouldBeUnique();
        }
    }
}
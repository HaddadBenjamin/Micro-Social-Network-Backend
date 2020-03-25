using DiabloII.Items.Api.Validators.Suggestions.Create;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions
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
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public class CreateASuggestionValidator : AbstractValidator<CreateASuggestionValidatorContext>
    {
        public CreateASuggestionValidator()
        {
            RuleFor(context => context.Dto.Content).ShouldNotBeNullOrEmpty("Content");
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidatorContext).SuggestionContentShouldBeUnique();
        }
    }
}
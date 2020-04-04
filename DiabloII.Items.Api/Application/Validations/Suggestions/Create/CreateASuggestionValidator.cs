using FluentValidation;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.Create
{
    public class CreateASuggestionValidator : AbstractValidator<CreateASuggestionValidationContext>
    {
        public CreateASuggestionValidator()
        {
            RuleFor(context => context.Dto.Content)
                .ShouldNotBeNullOrEmpty("Content")
                .ShouldBeShorterThan("Content");
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidationContext).SuggestionContentShouldBeUnique();
        }
    }
}
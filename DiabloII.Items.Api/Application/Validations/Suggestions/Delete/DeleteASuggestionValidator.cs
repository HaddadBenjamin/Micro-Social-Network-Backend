using FluentValidation;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.Delete
{
    public class DeleteASuggestionValidator : AbstractValidator<DeleteASuggestionValidationContext>
    {
        public DeleteASuggestionValidator()
        {
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidationContext)
                .SuggestionShouldExists()
                .SuggestionShouldBeRelatedToTheUserIp();
        }
    }
}
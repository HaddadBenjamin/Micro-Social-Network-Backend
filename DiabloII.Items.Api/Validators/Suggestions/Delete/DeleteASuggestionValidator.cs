using DiabloII.Items.Api.Validators.Suggestions.Vote;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Delete
{
    public class DeleteASuggestionValidator : AbstractValidator<DeleteASuggestionValidatorContext>
    {
        public DeleteASuggestionValidator()
        {
            RuleFor(context => context.Dto.Ip)
                .SuggestionIpShouldNotBeNullOrEmpty()
                .SuggestionIpShouldBeAnIpV4();

            RuleFor(context => context)
                .SuggestionShouldExistsAndBeRelatedToTheUserIp();
        }
    }
}